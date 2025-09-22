using fairino;
using RM.src.RM240513;
using RMLib.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Valve.VR;

/// <summary>
/// Gestisce strumentazione HTC VIVE
/// </summary>
public class OpenVRsupport
{
    #region Proprietà di OpenVRsupport

    /// <summary>
    /// Logger
    /// </summary>
    private static readonly log4net.ILog log = LogHelper.GetLogger();
    /// <summary>
    /// Oggetto sistema VR
    /// </summary>
    private CVRSystem vrSystem;
    /// <summary>
    /// Contiene la matrice di trasformazione del tracker rispetto al robot
    /// </summary>
    public Matrix4x4 _trackerToRobotTransformation =  Matrix4x4.Identity;
    /// <summary>
    /// Contiene il quaternione relativo del tracker per il calcolo della rotazione del tracker
    /// </summary>
    public Quaternion trackerReferenceQuat;     // salva qTracker alla calibrazione
    /// <summary>
    /// Contiene il quaternione di reference del robot per il calcolo della rotazione 
    /// </summary>
    public Quaternion rotationReferenceQuat;    // salva qRobot alla calibrazione
    /// <summary>
    /// Id del tracker
    /// </summary>
    public uint trackerId = 0;
    /// <summary>
    /// Posizione letta dal tracker HTC VIVE
    /// </summary>
    public static DescPose HTCVIVEInitialTrackerPosition = new DescPose(0,0,0,0,0,0);
    /// <summary>
    /// Posizione letta dal tracker HTC VIVE che viene aggiornata in tempo reale
    /// </summary>
    public  DescPose HTCVIVETrackerPosition = new DescPose(0, 0, 0, 0, 0, 0);
    /// <summary>
    /// Contiene il quaternion di offset di rotazione del tracker rispetto al robot
    /// </summary>
    public Quaternion rotationOffsetQuat = Quaternion.Identity;
    /// <summary>
    /// Angoli assoluti iniziali (in gradi)
    /// </summary>
    private Quaternion initialRotationQuaternion = Quaternion.Identity;
    /// <summary>
    /// Rappresenta l'angolo di rotazione del tracker rispetto al robot
    /// </summary>
    const double Rad2Deg = 180.0 / Math.PI;

    #region Struttura thread lettura coordinate

    /// <summary>
    /// Thread che legge le posizioni del tracker
    /// </summary>
    //private static Thread trackerPoseThread;
    /// <summary>
    /// Refresh delay del thread di lettura
    /// </summary>
    private const int trackerPoseRefreshPeriod = 200;

    #endregion

    #region Struttura live motion

    /// <summary>
    /// Abilita il thread a muovere il Robot in live motion
    /// </summary>
    public static bool startLiveMotion = false;
    /// <summary>
    /// Contiene la posizione del robot in tempo reale.
    /// </summary>
    public static DescPose RobotPoseLive = new DescPose(0, 0, 0, 0, 0, 0);
    /// <summary>
    /// Soglia per fare i controlli di in position 
    /// </summary>
    private const int threshold = 100; // 10 cm
    /// <summary>
    /// Checker usato per fare in modo da non averne punti abbastanza simili tra loro risolvendo il 
    /// problema della linear drag mode
    /// </summary>
    private readonly PositionChecker checkerPos = new PositionChecker(threshold);
    /// <summary>
    /// Quando i 2 punti sono simili sarà True
    /// </summary>
    private static bool isPositionMatch = false;
    /// <summary>
    /// Lista di punti da inviare al robot
    /// </summary>
    private static readonly List<DescPose> positionToSend = new List<DescPose>();
    /// <summary>
    /// Timer che gestisce l'invio delle posizioni al robot
    /// </summary>
    private System.Timers.Timer motionTimer;
    /// <summary>
    /// Indica se il movimento è partito
    /// </summary>
    private static bool motionStarted = false;

    #endregion

    #endregion

    #region Metodi di OpenVRsupport

    /// <summary>
    /// Esegue inizializzazione dell'oggetto VR e lancia thread per get delle coordinate dal tracker
    /// </summary>
    /// <returns></returns>
    public bool Initialize()
    {
        // Inizializza SteamVR
        EVRInitError initError = EVRInitError.None;
        OpenVR.Init(ref initError, EVRApplicationType.VRApplication_Other);

        // Controlla se l'inizializzazione ha avuto successo
        if (initError != EVRInitError.None)
        {
            log.Error($"Errore di inizializzazione: {initError}");
            return false;
        }

        // Ottieni il sistema VR dopo l'inizializzazione
        vrSystem = OpenVR.System;

        if (vrSystem == null)
        {
            log.Error("Errore: OpenVR.System è null!");
            return false;
        }

        log.Info("SteamVR inizializzato correttamente!");

        CheckTracker();

        // Avvia il thread per ottenere le pose del tracker
        RobotManager.taskManager.AddTask(nameof(GetTrackerPose), GetTrackerPose, TaskType.LongRunning, true);
        RobotManager.taskManager.StartTask(nameof(GetTrackerPose));

        return true;
    }

    /// <summary>
    /// Legge le coordinate del tracker HTC VIVE e le aggiorna in HTCVIVETrackerPosition.
    /// Effettua una traslazione di asse per allineare il piano di lavoro del tracker orizzontalmente. 
    /// Così è possibile posizionare il tracker in orizzontale sul tool.
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task GetTrackerPose(CancellationToken token)
    {
        try
        {
            if (vrSystem == null)
                throw new InvalidOperationException("OpenVR non inizializzato");

            // definisco qui il mountOffset di –90° attorno a X
            var mountOffsetQuat = Quaternion.CreateFromAxisAngle(Vector3.UnitX, (float)Math.PI / 2);
            var mountOffsetMat = Matrix4x4.CreateFromQuaternion(mountOffsetQuat);

            while (!token.IsCancellationRequested)
            {
                // 1) Recupera la pose
                var poses = new TrackedDevicePose_t[OpenVR.k_unMaxTrackedDeviceCount];
                vrSystem.GetDeviceToAbsoluteTrackingPose(
                    ETrackingUniverseOrigin.TrackingUniverseStanding,
                    0, poses);
                if (!poses[trackerId].bPoseIsValid)
                {
                    await Task.Delay(trackerPoseRefreshPeriod);
                    continue;
                }

                // 2) Matrice 3×3 e posizione raw (mm)
                var m = poses[trackerId].mDeviceToAbsoluteTracking;
                // costruisco rawMat 4×4
                var rawMat = new Matrix4x4(
                    m.m0, m.m1, m.m2, 0,
                    m.m4, m.m5, m.m6, 0,
                    m.m8, m.m9, m.m10, 0,
                    0, 0, 0, 1
                );
                float xRaw = m.m3 * 1000f;
                float yRaw = m.m7 * 1000f;
                float zRaw = m.m11 * 1000f;

                // applico il pre-align al frame del tracker
                Matrix4x4 alignedMat = mountOffsetMat * rawMat;
                Vector3 alignedPos = Vector3.Transform(new Vector3(xRaw, yRaw, zRaw), mountOffsetMat);

                // 3) Estrai Euler ZYX (yaw→pitch→roll) dall’alignedMat
                //    ricostruisco l’array R nella stessa convenzione row-major:
                float[] R = {
            alignedMat.M11, alignedMat.M12, alignedMat.M13,
            alignedMat.M21, alignedMat.M22, alignedMat.M23,
            alignedMat.M31, alignedMat.M32, alignedMat.M33
        };
                (double rzRaw, double ryRaw, double rxRaw) = MatrixToEulerZYX(R);

                // 4) Aggiorno UI e stato con i valori aligned
                RobotManager.viveTrackerPage.UpdateLbl_coord(
                    alignedPos.X, alignedPos.Y, alignedPos.Z,
                    (float)rxRaw, (float)ryRaw, (float)rzRaw);

                // 5) salvo aligned in oggetto di stato
                HTCVIVETrackerPosition = new DescPose(
                    alignedPos.X, alignedPos.Y, alignedPos.Z,
                    rxRaw, ryRaw, rzRaw);

                await Task.Delay(trackerPoseRefreshPeriod, token);
            }
            token.ThrowIfCancellationRequested();
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {

        }
    }
   
    /// <summary>
    /// Legge la posa del tracker (non usato)
    /// </summary>
    /// <returns></returns>
    public bool GetCurrentTrackerPose()
    {
        if (vrSystem == null)
        {
            log.Error("OpenVR non inizializzato");
            return false;
        }

        TrackedDevicePose_t[] poses = new TrackedDevicePose_t[OpenVR.k_unMaxTrackedDeviceCount];
        vrSystem.GetDeviceToAbsoluteTrackingPose(ETrackingUniverseOrigin.TrackingUniverseStanding, 0, poses);

        if (!poses[trackerId].bPoseIsValid)
        {
            log.Warn("Tracker non ha posa valida");
            return false;
        }

        HmdMatrix34_t m = poses[trackerId].mDeviceToAbsoluteTracking;

        // Traslazione in mm
        float x = m.m3 * 1000f;
        float y = m.m7 * 1000f;
        float z = m.m11 * 1000f;

        // Quaternion corrente da matrice 3x3 (rotazione)
        Quaternion currentQuat = MatrixToQuaternion(
            m.m0, m.m1, m.m2,
            m.m4, m.m5, m.m6,
            m.m8, m.m9, m.m10
        );
        currentQuat = Quaternion.Normalize(currentQuat);

        // Quaternion relativo rispetto alla rotazione iniziale
        Quaternion relativeQuat = Quaternion.Normalize(
            Quaternion.Multiply(
                Quaternion.Inverse(initialRotationQuaternion),
                currentQuat
            )
        );

        // Estrai angoli puri attorno agli assi locali X, Y, Z (in gradi)
        float ry = (float)Math.Round(GetPureRotationAngleAroundAxis(relativeQuat, 'X'), 3);
        float rx = (float)Math.Round(GetPureRotationAngleAroundAxis(relativeQuat, 'Y'), 3);   // Prima asse Y → rz, ora a rx
        float rz = (float)Math.Round(GetPureRotationAngleAroundAxis(relativeQuat, 'Z'), 3);   // Prima asse Z → rx, ora a rz

        HTCVIVETrackerPosition = new DescPose(x, y, z, rx, ry, rz);
        return true;
    }

    /// <summary>
    /// Calcola gli offset di posizione del tracker rispetto alla posizione iniziale.
    /// </summary>
    /// <returns></returns>
    public DescPose GetOffsetPosition()
    {
        // Ottieni la posizione attuale del tracker
        DescPose trackerPos = RobotManager.openVRsupport.HTCVIVETrackerPosition;

        // Calcola lo spostamento del tracker rispetto alla posizione iniziale
        double deltaX = trackerPos.tran.x - OpenVRsupport.HTCVIVEInitialTrackerPosition.tran.x;
        double deltaY = trackerPos.tran.y - OpenVRsupport.HTCVIVEInitialTrackerPosition.tran.y;
        double deltaZ = trackerPos.tran.z - OpenVRsupport.HTCVIVEInitialTrackerPosition.tran.z;

        double deltaRX = trackerPos.rpy.rx - OpenVRsupport.HTCVIVEInitialTrackerPosition.rpy.rx;
        double deltaRY = trackerPos.rpy.ry - OpenVRsupport.HTCVIVEInitialTrackerPosition.rpy.ry;
        double deltaRZ = trackerPos.rpy.rz - OpenVRsupport.HTCVIVEInitialTrackerPosition.rpy.rz;
        /*
        // Verifica se la variazione è significativa (almeno 5 cm per X, Y o Z)
        if (Math.Abs(deltaX) < 50 && Math.Abs(deltaY) < 50 && Math.Abs(deltaZ) < 50 && Math.Abs(deltaRX) < 10 && Math.Abs(deltaRY) < 10 && Math.Abs(deltaRZ) < 10)
        {
            Console.WriteLine("Movimento troppo piccolo, nessun aggiornamento.");
            return; // Esci senza aggiornare il robot
        }*/

        // Creazione dell'offset per il movimento di 200 mm lungo Z nel sistema del tool
        DescPose offsetPos = new DescPose(
            deltaX,     // Nessuno spostamento su X
            deltaY,     // Nessuno spostamento su Y
            deltaZ,  // Spostamento di -200 mm su Z
            deltaRX,     // Nessuna variazione su RX (rotazione)
            deltaRY,     // Nessuna variazione su RY (rotazione)
            deltaRZ      // Nessuna variazione su RZ (rotazione)
        );

        HTCVIVEInitialTrackerPosition = trackerPos;

        return offsetPos;
    }

    /// <summary>
    /// Rileva la presenza del tracker
    /// </summary>
    public void CheckTracker()
    {
        // Se l'oggetto VR è null
        if (vrSystem == null)
        {
            log.Error("Errore: OpenVR non è stato inizializzato!");
            return;
        }

        log.Info("Verifica dei dispositivi di tracciamento...");

        for (uint i = 0; i < OpenVR.k_unMaxTrackedDeviceCount; i++)
        {
            if (vrSystem.IsTrackedDeviceConnected(i))
            {
                ETrackedDeviceClass deviceClass = vrSystem.GetTrackedDeviceClass(i);

                if (deviceClass == ETrackedDeviceClass.GenericTracker)
                {
                    log.Info($"✅ Vive Tracker rilevato! ID: {i}");
                    // Assegno alla variabile statica il valore ID del tracker
                    trackerId = i;
                }
                else
                {
                    log.Info($"Dispositivo {i}: {deviceClass} (non un tracker)");
                }
            }
        }
    }

    public bool IsTrackerConnected()
    {
        // Se l'oggetto VR è null
        if (vrSystem == null)
        {
            log.Error("Errore: OpenVR non è stato inizializzato!");
            return false;
        }
        else
            return true;
    }

    /// <summary>
    /// Esegue chiusura connessione con strumentazione HTC VIVE
    /// </summary>
    public void Shutdown()
    {
        if (vrSystem != null)
        {
            OpenVR.Shutdown();
            Console.WriteLine("SteamVR chiuso correttamente.");
        }
    }

    #endregion

    #region Metodi Live Motion

    /// <summary>
    /// Esegue ciclo continuo per il live motion del Robot.
    /// </summary>
    public void ExecLiveMotion()
    {
        while (true)
        {
            if (startLiveMotion)
            {

                LiveMotion();

                if (!motionStarted)
                {
                    motionStarted = true;
                    StartMotion();
                }
            }

            Thread.Sleep(trackerPoseRefreshPeriod);
        }
    }

    /// <summary>
    /// Live motion del Robot strutturato in step
    /// </summary>
    public async void ExecLiveMotionByStep()
    {
        int step = 0; // step ciclo di liveMotion
        bool isTrackerPoseValid = false; // A true quando la posizione del tracker è valida
        DescPose offsetPos = new DescPose(0, 0, 0, 0, 0, 0); // Contiene gli offset di spostamento da applicare alla posizione del Robot
        int thresholdChecker = 50; // Soglia checker
        PositionChecker checkerRobotPos = new PositionChecker(thresholdChecker); // Checker per non inviare la stessa posizione
        bool isRobotPositionMatch = true; // A true quando la posizione da inviare al Robot combacia con l'attuale posizione
        DescPose lastRobotPose = new DescPose(0, 0, 0, 0, 0, 0);
        List<DescPose> motionBuffer = new List<DescPose>(); // Buffer per gestire le posizioni del robot
        int motionTreshold = 20; // Soglia di posizioni da avere in coda per far muovere il Robot


        while (true) // Faccio girare il metodo all'interno del thread
        {
            if (startLiveMotion) // Se è stata richiesto il liveMotion
            {
                switch (step)
                {
                    case 0:
                        #region Calcolo offset spostamento

                        // Calcolo l'offset dello spostamento
                        isTrackerPoseValid = GetCurrentTrackerPose();

                        step = 10;

                        break;
                    #endregion

                    case 10:
                        #region Check validità posa del Tracker

                        if (isTrackerPoseValid)
                            step = 20;
                        else
                            step = 0;

                        break;
                    #endregion

                    case 20:
                        #region Calcolo nuova posizione da inviare al Robot

                        offsetPos = GetOffsetPosition();

                        // Aggiorna la posizione esistente senza sovrascriverla
                        RobotPoseLive.tran.x += offsetPos.tran.x;
                        RobotPoseLive.tran.y += offsetPos.tran.y;
                        RobotPoseLive.tran.z += offsetPos.tran.z;


                        RobotPoseLive.rpy.rx = NormalizeAngle((float)(RobotPoseLive.rpy.rx + offsetPos.rpy.rx));
                        RobotPoseLive.rpy.ry = NormalizeAngle((float)(RobotPoseLive.rpy.ry + offsetPos.rpy.ry));
                        RobotPoseLive.rpy.rz = NormalizeAngle((float)(RobotPoseLive.rpy.rz + offsetPos.rpy.rz));

                        // Arrotondamento a 3 cifre decimali
                        RobotManager.RoundPositionDecimals(ref RobotPoseLive, 3);

                        step = 50;

                        #endregion
                        break;

                    case 30:
                        #region Calcolo inPosition posizione Robot

                        // Calcolo della differenza assoluta tra le rotazioni
                        double rotationDifferenceX = Math.Abs(RobotPoseLive.rpy.rx - lastRobotPose.rpy.rx);
                        double rotationDifferenceY = Math.Abs(RobotPoseLive.rpy.ry - lastRobotPose.rpy.ry);
                        double rotationDifferenceZ = Math.Abs(RobotPoseLive.rpy.rz - lastRobotPose.rpy.rz);

                        if (Math.Abs(RobotPoseLive.tran.x - lastRobotPose.tran.x) < 10 &&
                            Math.Abs(RobotPoseLive.tran.y - lastRobotPose.tran.y) < 10 &&
                            Math.Abs(RobotPoseLive.tran.z - lastRobotPose.tran.z) < 10 &&
                            Math.Abs(RobotPoseLive.rpy.rx - lastRobotPose.rpy.rx) < 10 &&
                            Math.Abs(RobotPoseLive.rpy.ry - lastRobotPose.rpy.ry) < 10 &&
                            Math.Abs(RobotPoseLive.rpy.rz - lastRobotPose.rpy.rz) < 10)
                        {
                            isRobotPositionMatch = true;
                        }
                        else
                        {
                            isRobotPositionMatch = false;
                        }

                        step = 40;

                        #endregion
                        break;

                    case 40:
                        #region Check spostamento Robot maggiore della soglia definita

                        if (isRobotPositionMatch)
                            step = 0;
                        else
                            step = 50;

                        break;
                    #endregion

                    case 50:
                        #region Invio movimenti al Robot

                        if (motionBuffer.Count >= motionTreshold) // Controllo che ci siano abbastanza posizioni in coda
                        {
                            foreach (var pose in motionBuffer)
                            {
                                int err = RobotManager.robot.MoveCart(pose, RobotManager.tool, RobotManager.user, RobotManager.vel, RobotManager.acc,
                                    RobotManager.ovl, RobotManager.blendT, RobotManager.config);

                                if (err != 0)
                                {
                                    Console.WriteLine($"Errore nel movimento: {err}");
                                    break;
                                }
                            }
                            motionBuffer.Clear(); // Svuota il buffer dopo l'invio
                        }
                        else
                        {
                            motionBuffer.Add(RobotPoseLive);
                        }

                        lastRobotPose = RobotPoseLive;
                        step = 0;
                        break;

                        #endregion

                }

            }

            Thread.Sleep(20); // delay del ciclo
        }
    }

    /// <summary>
    /// Esegue live motion del Robot strutturato in step. Usando movimento in tool
    /// </summary>
    public async void ExecLiveMotionByStepTool()
    {
        int step = 0; // step ciclo di liveMotion
        bool isTrackerPoseValid = false; // A true quando la posizione del tracker è valida
        DescPose offsetPos = new DescPose(0, 0, 0, 0, 0, 0); // Contiene gli offset di spostamento da applicare alla posizione del Robot
        int thresholdChecker = 50; // Soglia checker
        PositionChecker checkerRobotPos = new PositionChecker(thresholdChecker); // Checker per non inviare la stessa posizione
        bool isRobotPositionMatch = true; // A true quando la posizione da inviare al Robot combacia con l'attuale posizione
        DescPose lastRobotPose = new DescPose(0, 0, 0, 0, 0, 0);
        // Dichiarazione asse esterno (nessuno)
        ExaxisPos epos = new ExaxisPos(0, 0, 0, 0);

        while (true) // Faccio girare il metodo all'interno del thread
        {
            if (startLiveMotion) // Se è stata richiesto il liveMotion
            {
                switch (step)
                {
                    case 0:
                        #region Calcolo offset spostamento

                        // Calcolo l'offset dello spostamento
                        isTrackerPoseValid = GetCurrentTrackerPose();

                        step = 10;

                        break;
                    #endregion

                    case 10:
                        #region Check validità posa del Tracker

                        if (isTrackerPoseValid)
                            step = 20;
                        else
                            step = 0;

                        break;
                    #endregion

                    case 20:
                        #region Calcolo nuova posizione da inviare al Robot

                        offsetPos = GetOffsetPosition();

                        // Aggiorna la posizione esistente senza sovrascriverla
                        RobotPoseLive.tran.x += offsetPos.tran.x;
                        RobotPoseLive.tran.y += offsetPos.tran.y;
                        RobotPoseLive.tran.z += offsetPos.tran.z;


                        RobotPoseLive.rpy.rx = NormalizeAngle((float)(RobotPoseLive.rpy.rx + offsetPos.rpy.rx));
                        RobotPoseLive.rpy.ry = NormalizeAngle((float)(RobotPoseLive.rpy.ry + offsetPos.rpy.ry));
                        RobotPoseLive.rpy.rz = NormalizeAngle((float)(RobotPoseLive.rpy.rz + offsetPos.rpy.rz));

                        // Arrotondamento a 3 cifre decimali
                        RobotManager.RoundPositionDecimals(ref RobotPoseLive, 3);

                        step = 30;

                        #endregion
                        break;

                    case 30:
                        #region Calcolo inPosition posizione Robot
                        // Calcolo della differenza assoluta tra le rotazioni
                        double rotationDifferenceX = Math.Abs(RobotPoseLive.rpy.rx - lastRobotPose.rpy.rx);
                        double rotationDifferenceY = Math.Abs(RobotPoseLive.rpy.ry - lastRobotPose.rpy.ry);
                        double rotationDifferenceZ = Math.Abs(RobotPoseLive.rpy.rz - lastRobotPose.rpy.rz);

                        if (Math.Abs(RobotPoseLive.tran.x - lastRobotPose.tran.x) < 10 &&
                            Math.Abs(RobotPoseLive.tran.y - lastRobotPose.tran.y) < 10 &&
                            Math.Abs(RobotPoseLive.tran.z - lastRobotPose.tran.z) < 10 &&
                            Math.Abs(RobotPoseLive.rpy.rx - lastRobotPose.rpy.rx) < 10 &&
                            Math.Abs(RobotPoseLive.rpy.ry - lastRobotPose.rpy.ry) < 10 &&
                            Math.Abs(RobotPoseLive.rpy.rz - lastRobotPose.rpy.rz) < 10)
                        {
                            isRobotPositionMatch = true;
                        }
                        else
                        {
                            isRobotPositionMatch = false;
                        }

                        step = 40;

                        #endregion
                        break;

                    case 40:
                        #region Check spostamento Robot maggiore della soglia definita

                        if (isRobotPositionMatch)
                            step = 0;
                        else
                            step = 50;

                        break;
                    #endregion

                    case 50:
                        #region Movimento del Robot

                        // Creazione della posizione obiettivo con l'offset applicato
                        JointPos jointActualPose = new JointPos(0, 0, 0, 0, 0, 0);

                        // Calcolo posizione tool in joint
                        RobotManager.robot.GetInverseKin(0, RobotPoseLive, -1, ref jointActualPose);

                        // Movimento del robot con offset nel sistema del tool
                        int err = RobotManager.robot.MoveJ(jointActualPose, RobotPoseLive, RobotManager.tool,
                            RobotManager.user, RobotManager.vel, RobotManager.acc,
                            RobotManager.ovl, epos, RobotManager.blendT, 2, offsetPos);
                        // RobotManager.endingPoint = RobotPoseLive;
                        lastRobotPose = RobotPoseLive;

                        step = 0;
                        break;
                    #endregion

                    case 60:

                        await Task.Delay(50);
                        step = 70;

                        break;

                    case 70:

                        if (RobotManager.inPosition)
                            step = 0;

                        break;


                }

            }

            Thread.Sleep(100); // delay del ciclo
        }
    }

    /// <summary>
    /// Fa partire il timer per l'invio delle posizioni al robot.
    /// </summary>
    public void StartMotion()
    {
        motionTimer = new System.Timers.Timer(100); // Esegui ogni 100ms
        motionTimer.Elapsed += SendMotionPositions;
        motionTimer.AutoReset = true;
        motionTimer.Start();
    }

    /// <summary>
    /// Invia le posizioni al robot in move cart 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void SendMotionPositions(object sender, System.Timers.ElapsedEventArgs e)
    {
        if (positionToSend.Count > 0)
        {

            foreach (DescPose pose in positionToSend)
            {
                int err = RobotManager.robot.MoveCart(pose, RobotManager.tool, RobotManager.user, RobotManager.vel,
                    RobotManager.acc, RobotManager.ovl, RobotManager.blendT, RobotManager.config);
            }
            positionToSend.Clear();

            /*if (AlarmManager.blockingAlarm)
            {
                RobotManager.robot.ResetAllError();
                AlarmManager.blockingAlarm = false;
            }*/
        }

        //  if (AlarmManager.blockingAlarm)
        {
            // AlarmManager.blockingAlarm = false;
            RobotManager.robot.ResetAllError();
        }
    }

    /// <summary>
    /// Legge la posizione del tracker, applica degli offset e invia la posizione al robot in live motion.
    /// </summary>
    public void LiveMotion()
    {
        // Calcolo l'offset dello spostamento
        bool isValid = GetCurrentTrackerPose();

        if (isValid)
        {
            try
            {
                DescPose offsetPos = GetOffsetPosition();

                // Aggiorna la posizione esistente senza sovrascriverla
                RobotPoseLive.tran.x += offsetPos.tran.x;
                RobotPoseLive.tran.y += offsetPos.tran.y;
                RobotPoseLive.tran.z += offsetPos.tran.z;

                RobotPoseLive.rpy.rx = NormalizeAngle((float)(RobotPoseLive.rpy.rx + offsetPos.rpy.rx));
                RobotPoseLive.rpy.ry = NormalizeAngle((float)(RobotPoseLive.rpy.ry + offsetPos.rpy.ry));
                RobotPoseLive.rpy.rz = NormalizeAngle((float)(RobotPoseLive.rpy.rz + offsetPos.rpy.rz));

                // Arrotondamento a 3 cifre decimali
                RobotManager.RoundPositionDecimals(ref RobotPoseLive, 3);

                // Se ci sono dei punti salvati allora posso usare l'ultimo punto per fare il controllo inPosition
                if (positionToSend.Count > 0)
                    isPositionMatch = checkerPos.IsInPosition(RobotPoseLive, positionToSend.Last());

                // Se le posizioni sono diverse le salvo e genero l'evento per la list view
                if (!isPositionMatch)
                {
                    log.Info($"Posizione tracker -- > " +
                        $"X: {RobotManager.openVRsupport.HTCVIVETrackerPosition.tran.x} - " +
                        $"Y: {RobotManager.openVRsupport.HTCVIVETrackerPosition.tran.y} - " +
                        $"Z: {RobotManager.openVRsupport.HTCVIVETrackerPosition.tran.z} ");

                    log.Info($"Offset --> " +
                        $"X: {offsetPos.tran.x} - " +
                        $"Y: {offsetPos.tran.y} - " +
                        $"Z: {offsetPos.tran.z} ");

                    log.Info($"Posizione salvata -- > " +
                        $"X: {RobotPoseLive.tran.x} - " +
                        $"Y: {RobotPoseLive.tran.y} - " +
                        $"Z: {RobotPoseLive.tran.z} ");


                    positionToSend.Add(RobotPoseLive);

                    /*
                   if (positionToSend.Count > 0)
                   {
                        foreach (DescPose pose in positionToSend)
                        {
                            // Invio delle posizione al robot
                            int err = RobotManager.robot.MoveCart(pose, RobotManager.tool, RobotManager.user, RobotManager.vel, RobotManager.acc, 
                                RobotManager.ovl, RobotManager.blendT, RobotManager.config);
                        }

                        positionToSend.Clear();
                       /* if (AlarmManager.blockingAlarm)
                        {
                            RobotManager.robot.ResetAllError();
                            AlarmManager.blockingAlarm = false;
                        }*/
                    // }

                }
            }

            catch (Exception ex)
            {

            }
        }

    }

    #endregion

    #region Helpers

    /// <summary>
    /// Converte una matrice 3x3 in angoli di Eulero ZYX (yaw, pitch, roll).
    /// </summary>
    /// <param name="M"></param>
    /// <returns></returns>
    static (double rz, double ry, double rx) MatrixToEulerZYX(float[] M)
    {
        // M = [ m00 m01 m02
        //       m10 m11 m12
        //       m20 m21 m22 ]
        double m00 = M[0], m01 = M[1], m02 = M[2];
        double m10 = M[3], m11 = M[4], m12 = M[5];
        double m20 = M[6], m21 = M[7], m22 = M[8];

        // yaw  = Z
        double yaw = Math.Atan2(m10, m00) * Rad2Deg;
        // pitch= Y (con salto netto +180→–180)
        double pitch = Math.Atan2(
            -m20,
            Math.Sqrt(m00 * m00 + m10 * m10)
        ) * Rad2Deg;
        // roll = X
        double roll = Math.Atan2(m21, m22) * Rad2Deg;

        return (Norm180(yaw),
                 Norm180(pitch),
                 Norm180(roll));
    }

    /// <summary>
    /// Normalizza un angolo in gradi nell'intervallo [-180, 180].
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    static double Norm180(double a)
    {
        a %= 360;
        if (a > 180) a -= 360;
        if (a < -180) a += 360;
        return a;
    }

    /// <summary>
    /// Estrae l'angolo puro attorno a un asse LOCALE ('X','Y','Z')
    /// </summary>
    /// <param name="q"></param>
    /// <param name="axis"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    float GetPureRotationAngleAroundAxis(Quaternion q, char axis)
    {
        // 1) Calcola l'asse locale trasformato in globale
        Vector3 localAxis;
        switch (axis)
        {
            case 'X':
                localAxis = Vector3.Normalize(Vector3.Transform(Vector3.UnitX, initialRotationQuaternion));
                break;
            case 'Y':
                localAxis = Vector3.Normalize(Vector3.Transform(Vector3.UnitY, initialRotationQuaternion));
                break;
            case 'Z':
                localAxis = Vector3.Normalize(Vector3.Transform(Vector3.UnitZ, initialRotationQuaternion));
                break;
            default:
                throw new ArgumentException("Axis must be X, Y or Z");
        }

        // 2) Decomponi il quaternion in angolo e asse
        float angleRad = 2f * (float)Math.Acos(q.W);
        float sinHalf = (float)Math.Sqrt(1f - q.W * q.W);
        Vector3 qAxis = sinHalf < 1e-4f
            ? new Vector3(1, 0, 0)
            : Vector3.Normalize(new Vector3(q.X / sinHalf, q.Y / sinHalf, q.Z / sinHalf));

        // 3) Proietta l'angolo su localAxis
        float projection = Vector3.Dot(qAxis, localAxis);
        float pureAngleRad = projection * angleRad;
        float pureAngleDeg = pureAngleRad * 180f / (float)Math.PI;

        // 4) Normalizza in [-180,180]
        if (pureAngleDeg > 180f) pureAngleDeg -= 360f;
        else if (pureAngleDeg < -180f) pureAngleDeg += 360f;

        return pureAngleDeg;
    }

    /// <summary>
    /// Converte una matrice 3x3 in un quaternion.
    /// </summary>
    /// <param name="m00"></param>
    /// <param name="m01"></param>
    /// <param name="m02"></param>
    /// <param name="m10"></param>
    /// <param name="m11"></param>
    /// <param name="m12"></param>
    /// <param name="m20"></param>
    /// <param name="m21"></param>
    /// <param name="m22"></param>
    /// <returns></returns>
    Quaternion MatrixToQuaternion(
        float m00, float m01, float m02,
        float m10, float m11, float m12,
        float m20, float m21, float m22)
    {
        float trace = m00 + m11 + m22;
        float qw, qx, qy, qz;

        if (trace > 0f)
        {
            float s = 0.5f / (float)Math.Sqrt(trace + 1f);
            qw = 0.25f / s;
            qx = (m21 - m12) * s;
            qy = (m02 - m20) * s;
            qz = (m10 - m01) * s;
        }
        else if (m00 > m11 && m00 > m22)
        {
            float s = 2f * (float)Math.Sqrt(1f + m00 - m11 - m22);
            qw = (m21 - m12) / s;
            qx = 0.25f * s;
            qy = (m01 + m10) / s;
            qz = (m02 + m20) / s;
        }
        else if (m11 > m22)
        {
            float s = 2f * (float)Math.Sqrt(1f + m11 - m00 - m22);
            qw = (m02 - m20) / s;
            qx = (m01 + m10) / s;
            qy = 0.25f * s;
            qz = (m12 + m21) / s;
        }
        else
        {
            float s = 2f * (float)Math.Sqrt(1f + m22 - m00 - m11);
            qw = (m10 - m01) / s;
            qx = (m02 + m20) / s;
            qy = (m12 + m21) / s;
            qz = 0.25f * s;
        }

        return new Quaternion(qx, qy, qz, qw);
    }

    /// <summary>
    /// Funzione per normalizzare l'angolo tra -180° e 180°
    /// </summary>
    /// <param name="angle"></param>
    /// <returns></returns>
    private static float NormalizeAngle(float angle)
    {
        while (angle > 180) angle -= 360;
        while (angle <= -180) angle += 360;
        return angle;
    }


    #endregion
}
