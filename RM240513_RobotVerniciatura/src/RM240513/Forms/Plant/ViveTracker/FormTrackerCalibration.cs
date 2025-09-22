using fairino;
using log4net;
using MathNet.Numerics.LinearAlgebra;
using RMLib.Logger;
using RMLib.MessageBox;
using RMLib.Security;
using System;
using System.Linq;
using System.Numerics;
using System.Threading;
using System.Windows.Forms;

namespace RM.src.RM240513.Forms
{
    /// <summary>
    /// Gestisce la calibrazione del tracker htc vive 3.0.
    /// Usa l'algoritmo KABSCH con 3 punti sugli assi (origine, punto su asse x, punto su asse z)
    /// Crea una matrice di traslazione basandosi sul punto del tracker e il punto del robot (devono essere nella posizione prefissata)
    /// Crea una matrice di rotazione (quaternione)
    /// Imposta quaternione e matrice di traslazione nei rispettivi campi in robot manager per essere usati successivamente.
    /// Fornisce i metodi per traslare i punti dal frame del tracker al frame del robot, comprese le rotazioni.
    /// Il tracker per essere posizionato in orizzontale sul tool ha bisogno di aver girato il proprio piano di lavoro di 90° su asse x.
    /// </summary>
    public partial class FormTrackerCalibration : Form
    {
        #region Proprietà di FormTrackerCalibration

        /// <summary>
        /// Logger predefinito
        /// </summary>
        private static readonly ILog log = LogHelper.GetLogger();
        /// <summary>
        /// Definisce gli step della procedura di calibrazione
        /// </summary>
        private enum CalibrationStep { Origin, XAxis, ZAxis, Done }
        /// <summary>
        /// Definizione dello step corrente della calibrazione
        /// </summary>
        private CalibrationStep _currentStep = CalibrationStep.Origin;
        /// <summary>
        /// Vettore dei punti (x,y,z) del tracker
        /// </summary>
        private readonly Vector3[] _trackerPoints = new Vector3[3];
        /// <summary>
        /// Vettore dei punti (x,y,z) del robot
        /// </summary>
        private readonly Vector3[] _robotPoints = new Vector3[3];
        /// <summary>
        /// Vettore delle rotazioni del tracker (rx,ry,rz)
        /// </summary>
        private readonly Vector3[] _trackerRotations = new Vector3[3];
        /// <summary>
        /// Vettore delle rotazioni del tracker (rx,ry,rz)
        /// </summary>
        private readonly Vector3[] _robotRotations = new Vector3[3];
        /// <summary>
        /// Indica se è stata effettuata la calibrazione
        /// </summary>
        public bool isCalibrated = false;
        /// <summary>
        /// Ultima posa del tracker trasformata
        /// </summary>
        public DescPose lastTrackerPose = new DescPose();
        /// <summary>
        /// Posa corrente del tracker trasformata
        /// </summary>
        public DescPose trackerTransformedCurrentPose = new DescPose();
        /// <summary>
        /// Indica se il thread è abilitato a continuare la sua esecuzione
        /// </summary>
        private bool shouldContinueThread = true;

        #region Struttura thread lettura coordinate
        /// <summary>
        /// Definizione del thread che legge le pose del tracker
        /// </summary>
        private static Thread trackerPoseThread;
        /// <summary>
        /// Delay di refresh del thread
        /// </summary>
        private const int trackerPoseRefreshPeriod = 200;
        #endregion

        #endregion

        /// <summary>
        /// Costruisce una form per la calibrazione del tracker
        /// </summary>
        public FormTrackerCalibration()
        {
            InitializeComponent();
            SetupUI();
        }

        #region Metodi di FormTrackerCalibration

        /// <summary>
        /// Inizializza la UI
        /// </summary>
        private void SetupUI()
        {
            btnRecordX.Enabled = false;
            btnRecordZ.Enabled = false;
            btnConfirmCalibration.Enabled = false;
            lblStatus.Text = "Posiziona il tracker sull'ORIGINE del robot e clicca 'Registra Origine'";
            progressCalibration.Value = 0;
#if DEBUG
            btnTestPoint.Enabled = true;
#else
            btnTestPoint.Enabled = false;
#endif
        }

        /// <summary>
        /// Scrive un messaggio di log nella text box
        /// </summary>
        /// <param name="message"></param>
        private void LogMessage(string message)
        {
            txtLog.AppendText($"{message}{Environment.NewLine}");
        }

        /// <summary>
        /// Aggiorna lo stato della UI in base allo step di calibrazione
        /// </summary>
        /// <param name="step"></param>
        /// <param name="nextButton"></param>
        /// <param name="progress"></param>
        /// <param name="status"></param>
        private void AdvanceUI(CalibrationStep step, Button nextButton, int progress, string status)
        {
            _currentStep = step;
            nextButton.Enabled = true;
            progressCalibration.Value = progress;
            lblStatus.Text = status;
        }

        #endregion

        #region Eventi di FormTrackerCalibration

        /// <summary>
        /// Registra la coordinata di origine
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_recordOrigin(object sender, EventArgs e)
        {
            // lettura della posizione del tracker e del robot
            _trackerPoints[0] = DescPoseTranToVector3(RobotManager.openVRsupport.HTCVIVETrackerPosition);
            _robotPoints[0] = new Vector3(
                  (float)RobotManager.TCPCurrentPosition.tran.x,
                  (float)RobotManager.TCPCurrentPosition.tran.y,
                  (float)RobotManager.TCPCurrentPosition.tran.z
                );
            // lettura della rotazione del tracker e del robot
            _trackerRotations[0] = DescPoseToEulerZYX(RobotManager.openVRsupport.HTCVIVETrackerPosition);
            _robotRotations[0] = new Vector3(
                  (float)RobotManager.TCPCurrentPosition.rpy.rx,
                  (float)RobotManager.TCPCurrentPosition.rpy.ry,
                  (float)RobotManager.TCPCurrentPosition.rpy.rz
                );

            LogMessage($"Origine registrata - punto tracker: {_trackerPoints[0]}, punto Robot: {_robotPoints[0]}");
            LogMessage($"Origine registrata - rot tracker: {_trackerRotations[0]}, rot Robot: {_robotRotations[0]}");

            AdvanceUI(CalibrationStep.XAxis, btnRecordX, 33, "Muovi il tracker sull'ASSE X del robot e clicca 'Registra X'");
        }

        /// <summary>
        /// Registra la coordinata del punto sull'asse x
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_recordAxisX(object sender, EventArgs e)
        {
            // lettura della posizione del tracker e del robot
            _trackerPoints[1] = DescPoseTranToVector3(RobotManager.openVRsupport.HTCVIVETrackerPosition);
            _robotPoints[1] = new Vector3(
                  (float)RobotManager.TCPCurrentPosition.tran.x,
                  (float)RobotManager.TCPCurrentPosition.tran.y,
                  (float)RobotManager.TCPCurrentPosition.tran.z
                );
            // lettura della rotazione del tracker e del robot
            _trackerRotations[1] = DescPoseToEulerZYX(RobotManager.openVRsupport.HTCVIVETrackerPosition);
            _robotRotations[1] = new Vector3(
                  (float)RobotManager.TCPCurrentPosition.rpy.rx,
                  (float)RobotManager.TCPCurrentPosition.rpy.ry,
                  (float)RobotManager.TCPCurrentPosition.rpy.rz
                );

            LogMessage($"Punto asse x registrato - punto tracker: {_trackerPoints[1]}, punto Robot: {_robotPoints[1]}");
            LogMessage($"Punto asse x registrato - rot tracker: {_trackerRotations[1]}, rot Robot: {_robotRotations[1]}");

            AdvanceUI(CalibrationStep.ZAxis, btnRecordZ, 66, "Muovi il tracker sull'ASSE Z del robot e clicca 'Registra Z'");
        }

        /// <summary>
        /// Registra la coordinata del punto sull'asse Z
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_recordAxisZ(object sender, EventArgs e)
        {
            // lettura della posizione del tracker e del robot
            _trackerPoints[2] = DescPoseTranToVector3(RobotManager.openVRsupport.HTCVIVETrackerPosition);
            _robotPoints[2] = new Vector3(
                  (float)RobotManager.TCPCurrentPosition.tran.x,
                  (float)RobotManager.TCPCurrentPosition.tran.y,
                  (float)RobotManager.TCPCurrentPosition.tran.z
                );
            // lettura della rotazione del tracker e del robot
            _trackerRotations[2] = DescPoseToEulerZYX(RobotManager.openVRsupport.HTCVIVETrackerPosition);
            _robotRotations[2] = new Vector3(
                  (float)RobotManager.TCPCurrentPosition.rpy.rx,
                  (float)RobotManager.TCPCurrentPosition.rpy.ry,
                  (float)RobotManager.TCPCurrentPosition.rpy.rz
                );

            LogMessage($"Punto asse z registrato - punto tracker: {_trackerPoints[2]}, punto Robot: {_robotPoints[2]}");
            LogMessage($"Punto asse z registrato - rot tracker: {_trackerRotations[2]}, rot Robot: {_robotRotations[2]}");

            AdvanceUI(CalibrationStep.Done, btnConfirmCalibration, 100, "Calibrazione completata! Clicca 'Conferma' per salvare.");
        }

        /// <summary>
        /// Conferma dei punti di calibrazione e calcolo matrice di traslazione e matrice di rotazione
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_confirmCalibration(object sender, EventArgs e)
        {
            try
            {
                // 1) Pose snapshot per calibrazione
                DescPose tp = RobotManager.openVRsupport.HTCVIVETrackerPosition;
                DescPose rp = RobotManager.TCPCurrentPosition;

                // 2) Genera matrice 4×4 di allineamento (rotazione + traslazione)
                var M = CalculateAlignmentWithKabsch(_trackerPoints, _robotPoints);
                RobotManager.openVRsupport._trackerToRobotTransformation = M;

                // 3) Estrai solo la rotazione pura e trasformala in quaternion
                //     (la traslazione è in M.M41–M43, non ci serve per l’offset di orientamento)
                Matrix4x4 Rrot = M;
                Rrot.M41 = Rrot.M42 = Rrot.M43 = 0;
                Quaternion qMat = Quaternion.CreateFromRotationMatrix(Rrot);

                // 4) Leggi quaternion di tracker e robot al momento della calibrazione
                Quaternion qTrackerCalib = QuaternionFromRPY(
                    tp.rpy.rx, tp.rpy.ry, tp.rpy.rz);
                Quaternion qRobotCalib = QuaternionFromRPY(
                    rp.rpy.rx, rp.rpy.ry, rp.rpy.rz);

                // 5) Calcola l’offset quaternion in un’unica moltiplicazione:
                //     vogliamo che: qRobotCalib = rotationOffset * (qMat * qTrackerCalib)
                RobotManager.openVRsupport.rotationOffsetQuat =
                    qRobotCalib * Quaternion.Inverse(qMat * qTrackerCalib);

                // Imposta l'ultima posa del tracker trasformata con quella attuale
                lastTrackerPose = GetActualTransformedTrackerPose();

                LogMessage("=== Calibrazione completata ===");
                LogMessage($"Matrice 4×4:\n{MatrixToString(M)}");
                LogMessage($"OffsetQuat:\n{RobotManager.openVRsupport.rotationOffsetQuat}");

                // Imposta la calibrazione avvenuta
                isCalibrated = true;
            }
            catch (Exception ex)
            {
                LogMessage($"ERRORE CALIBRAZIONE: {ex.Message}");
                MessageBox.Show("Calibrazione fallita", "Errore",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                isCalibrated = false;
            }
        }

        /// <summary>
        /// Esegue un test di movimento sul frame calcolato in base al punto corrente del tracker (traslato)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_testPoint(object sender, EventArgs e)
        {
            if(!isCalibrated)
            {
                CustomMessageBox.Show(MessageBoxTypeEnum.WARNING_OK, "Calibrazione non completata");
                return;
            }
            // 1. Lettura la posa attuale del tracker trasformato
            DescPose trackerPose = GetActualTransformedTrackerPose();

            // 2. Calcolo la differenza rispetto all'ultima rotazione inviata
            float delta_rx = (float)(trackerPose.rpy.rx - lastTrackerPose.rpy.rx);
            float delta_ry = (float)(trackerPose.rpy.ry - lastTrackerPose.rpy.ry);
            float delta_rz = (float)(trackerPose.rpy.rz - lastTrackerPose.rpy.rz);

            // 3. Correzione degli assi (formula ottenuta tramite test di movimento)
            float corrected_delta_rx = -delta_rz;
            float corrected_delta_ry = -delta_rx;
            float corrected_delta_rz = delta_ry;

            // 4. Calcola la nuova pose da inviare partendo dalla vecchia
            DescPose poseToSend = new DescPose(0, 0, 0, 0, 0, 0);
            poseToSend.tran.x = trackerPose.tran.x;
            poseToSend.tran.y = trackerPose.tran.y;
            poseToSend.tran.z = trackerPose.tran.z;
            poseToSend.rpy.rx = NormalizeAngle((float)lastTrackerPose.rpy.rx + corrected_delta_rx);
            poseToSend.rpy.ry = NormalizeAngle((float)lastTrackerPose.rpy.ry + corrected_delta_ry);
            poseToSend.rpy.rz = NormalizeAngle((float)lastTrackerPose.rpy.rz + corrected_delta_rz);

            // 5. Conferma invio punto
            if (MessageBox.Show("Inviare punto?", "Test", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                // Imposto velocità di sicurezza
                RobotManager.robot.SetSpeed(15);

                // Invio punto al robot in move cart
                int err = RobotManager.robot.MoveCart(poseToSend, RobotManager.tool, RobotManager.user, 10, 10,
                                                       RobotManager.ovl, RobotManager.blendT, RobotManager.config);

                if (err == 0)
                    lastTrackerPose = poseToSend;
                else 
                    // 6. Aggiorna la rotazione "precedente" con la nuova inviata
                    lastTrackerPose = trackerPose;  // NOTA: salva il trackerPose attuale, non la poseToSend
            }
        }

        /// <summary>
        /// Esegue il thread che legge le pose del tracker e aggiorna le label
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_viewNewFrame(object sender, EventArgs e)
        {
            if(isCalibrated)
            {
                trackerPoseThread = new Thread(new ThreadStart(GetTrackerPose))
                {
                    IsBackground = true,
                    Priority = ThreadPriority.Lowest
                };
                trackerPoseThread.Start();
            }
            else
            {
                CustomMessageBox.Show(MessageBoxTypeEnum.WARNING_OK, "Calibrazione non completata");
            }

        }

        /// <summary>
        /// Effettua la calibrazione del tracker con i punti predefiniti
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_getLastFrame(object sender, EventArgs e)
        {

            // 1) Matrice 4×4 (row-vector)
            var M = new Matrix4x4(
                /* M11..M14 */ -0.049f, 0.999f, -0.015f, 0.000f,
                /* M21..M24 */ -0.999f, -0.049f, -0.017f, 0.000f,
                /* M31..M34 */ -0.017f, 0.014f, 1.000f, 0.000f,
                /* M41..M44 */ 1206.227f, -151.599f, 1258.842f, 1.000f
            );

            // 2) Quaternion di offset (x,y,z,w)
            var qOffset = new Quaternion(
                /* X = */ -0.5187244f,
                /* Y = */ 0.3999975f,
                /* Z = */ 0.4574127f,
                /* W = */ 0.6014157f
            );

            // 3) Inietta nei manager e metti isCalibrated = true:
            RobotManager.openVRsupport._trackerToRobotTransformation = M;
            RobotManager.openVRsupport.rotationOffsetQuat = qOffset;

            // Assegnazione last tracker pose
            lastTrackerPose = GetActualTransformedTrackerPose();

            LogMessage("=== Calibrazione completata ===");
            LogMessage($"Matrice 4×4:\n{MatrixToString(M)}");
            LogMessage($"OffsetQuat:\n{RobotManager.openVRsupport.rotationOffsetQuat}");

            // Segnalazione di avvenuta calibrazione
            isCalibrated = true;

            //CustomMessageBox.Show(MessageBoxTypeEnum.WARNING_OK, "Opzione non implementata");
        }

        /// <summary>
        /// Evento generato alla chiusura della form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormClosingEvent(object sender, FormClosingEventArgs e)
        {
            shouldContinueThread = false;
            if (trackerPoseThread != null && trackerPoseThread.IsAlive)
            {
                trackerPoseThread.Join();
            }
        }

        #endregion

        #region Calcolo rototraslazione

        /// <summary>
        /// Recupera la posa attuale del tracker trasformata in base alla matrice di traslazione e rotazione
        /// </summary>
        /// <returns></returns>
        public DescPose GetActualTransformedTrackerPose()
        {
            // 1) Leggi la pose grezza
            DescPose tp = RobotManager.openVRsupport.HTCVIVETrackerPosition;
            Vector3 rawPos = DescPoseTranToVector3(tp);
            Vector3 rawRpy = DescPoseRpyToVector3(tp);

            // 2) Applica la matrice completa (row-vector p′ = p*M)
            Matrix4x4 Mfull = RobotManager.openVRsupport._trackerToRobotTransformation;
            Vector3 wP = ApplyTransformation(rawPos, Mfull);

            // 3) Ricava il quaternion “live” del tracker
            Quaternion qTrackerNow = QuaternionFromRPY(
                rawRpy.X, rawRpy.Y, rawRpy.Z);

            // 4) Estrai la sola parte rotazionale di Mfull e convertila in quaternion
            Matrix4x4 Rrot = Mfull;
            Rrot.M41 = Rrot.M42 = Rrot.M43 = 0;
            Quaternion qMat = Quaternion.CreateFromRotationMatrix(Rrot);

            // 5) Unisci le tre rotazioni: 
            //    prima l’allineamento di frame (qMat), 
            //    poi l’offset di calibrazione (rotationOffsetQuat), 
            //    infine la rotazione “live”
            Quaternion qEst = RobotManager.openVRsupport.rotationOffsetQuat
                            * (qMat * qTrackerNow);

            Vector3 estRpy = RPYFromQuaternion(qEst);

            return new DescPose(wP.X, wP.Y, wP.Z, estRpy.X, estRpy.Y, estRpy.Z);
        }

        /// <summary>
        /// Metodo eseguito dal thread che legge le pose del tracker e aggiorna le label
        /// </summary>
        private void GetTrackerPose()
        {
            while (shouldContinueThread)
            {
                // 1) Leggi la pose grezza
                DescPose tp = RobotManager.openVRsupport.HTCVIVETrackerPosition;
                Vector3 rawPos = DescPoseTranToVector3(tp);
                Vector3 rawRpy = DescPoseRpyToVector3(tp);

                // 2) Applica la matrice completa (row-vector p′ = p*M)
                Matrix4x4 Mfull = RobotManager.openVRsupport._trackerToRobotTransformation;
                Vector3 wP = ApplyTransformation(rawPos, Mfull);

                // 3) Ricava il quaternion “live” del tracker
                Quaternion qTrackerNow = QuaternionFromRPY(
                    rawRpy.X, rawRpy.Y, rawRpy.Z);

                // 4) Estrai la sola parte rotazionale di Mfull e convertila in quaternion
                Matrix4x4 Rrot = Mfull;
                Rrot.M41 = Rrot.M42 = Rrot.M43 = 0;
                Quaternion qMat = Quaternion.CreateFromRotationMatrix(Rrot);

                // 5) Unisci le tre rotazioni
                Quaternion qEst = RobotManager.openVRsupport.rotationOffsetQuat
                                * (qMat * qTrackerNow);

                // 6) Converti in RPY
                Vector3 estRpy = RPYFromQuaternion(qEst);

                // === Aggiunta logica correzione assi ===

                // Calcola delta rispetto all'ultima posizione salvata
                float delta_rx = estRpy.X - (float)lastTrackerPose.rpy.rx;
                float delta_ry = estRpy.Y - (float)lastTrackerPose.rpy.ry;
                float delta_rz = estRpy.Z - (float)lastTrackerPose.rpy.rz;

                // Correggi gli assi (la formula è stata ottenuta tramite test di movimento)
                float corrected_delta_rx = -delta_rz;
                float corrected_delta_ry = -delta_rx;
                float corrected_delta_rz = delta_ry;

                // Calcola i valori corretti da mostrare
                float corrected_rx = NormalizeAngle((float)lastTrackerPose.rpy.rx + corrected_delta_rx);
                float corrected_ry = NormalizeAngle((float)lastTrackerPose.rpy.ry + corrected_delta_ry);
                float corrected_rz = NormalizeAngle((float)lastTrackerPose.rpy.rz + corrected_delta_rz);

                // Aggiorna le label con i valori corretti
                lblX.Text = wP.X.ToString("F2");
                lblY.Text = wP.Y.ToString("F2");
                lblZ.Text = wP.Z.ToString("F2");
                lblRX.Text = corrected_rx.ToString("F2");
                lblRY.Text = corrected_ry.ToString("F2");
                lblRZ.Text = corrected_rz.ToString("F2");

                // Aggiorna tracker pose attuale con la "live"
                trackerTransformedCurrentPose = new DescPose(wP.X, wP.Y, wP.Z, corrected_rx, corrected_ry, corrected_rz);

                Thread.Sleep(trackerPoseRefreshPeriod);
            }
        }

        /// <summary>
        /// Esegue l'algoritmo Kabsch per calcolare la matrice di allineamento tra i punti del tracker e quelli del robot
        /// </summary>
        /// <param name="tracker"></param>
        /// <param name="robot"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private Matrix4x4 CalculateAlignmentWithKabsch(Vector3[] tracker, Vector3[] robot)
        {
            if (tracker.Length != robot.Length)
                throw new ArgumentException("Devono essere lo stesso numero di punti");

            int n = tracker.Length;
            // 1) Centroidi
            var cT = new Vector3(
                tracker.Average(p => p.X),
                tracker.Average(p => p.Y),
                tracker.Average(p => p.Z)
            );

            var cR = new Vector3(
                robot.Average(p => p.X),
                robot.Average(p => p.Y),
                robot.Average(p => p.Z)
            );

            // 2) Costruisci matrici 3×N centrate
            var P = Matrix<float>.Build.Dense(3, n, (i, j) => new[]{
                tracker[j].X - cT.X,
                tracker[j].Y - cT.Y,
                tracker[j].Z - cT.Z
            }[i]);

            var Q = Matrix<float>.Build.Dense(3, n, (i, j) => new[]{
                robot[j].X - cR.X,
                robot[j].Y - cR.Y,
                robot[j].Z - cR.Z
            }[i]);

            // 3) Covarianza e SVD
            var H = P * Q.Transpose();
            var svd = H.Svd(computeVectors: true);
            var U = svd.U;
            var Vt = svd.VT;

            // 4) Rotazione “true”
            var Rmat = Vt.Transpose() * U.Transpose();
            // correzione se det<0
            if (Rmat.Determinant() < 0)
            {
                Vt.SetRow(2, Vt.Row(2).Multiply(-1));
                Rmat = Vt.Transpose() * U.Transpose();
            }

            // 5) Traslazione
            var RcT = Rmat * Vector<float>.Build.DenseOfArray(new[] { cT.X, cT.Y, cT.Z });
            Vector3 t = new Vector3(
                cR.X - RcT[0],
                cR.Y - RcT[1],
                cR.Z - RcT[2]
            );
            
            // 6) Componi la 4×4 per row-vector:
            //    M.M11..M13 = Rmatᵀ row0
            //    M.M21..M23 = Rmatᵀ row1
            //    M.M31..M33 = Rmatᵀ row2
            //    M.M41..M43 = t
            return new Matrix4x4(
                Rmat[0, 0], Rmat[1, 0], Rmat[2, 0], 0,   // M11 M12 M13 M14
                Rmat[0, 1], Rmat[1, 1], Rmat[2, 1], 0,   // M21 M22 M23 M24
                Rmat[0, 2], Rmat[1, 2], Rmat[2, 2], 0,   // M31 M32 M33 M34
                t.X, t.Y, t.Z, 1    // M41 M42 M43 M44
            );
        }

        /// <summary>
        /// Applica la trasformazione di una matrice 4x4 a un punto 3D (vettore)
        /// </summary>
        /// <param name="p"></param>
        /// <param name="M"></param>
        /// <returns></returns>
        private Vector3 ApplyTransformation(Vector3 p, Matrix4x4 M)
        {
            // row-vector: p' = p * M
            return new Vector3(
                p.X * M.M11 + p.Y * M.M21 + p.Z * M.M31 + M.M41,
                p.X * M.M12 + p.Y * M.M22 + p.Z * M.M32 + M.M42,
                p.X * M.M13 + p.Y * M.M23 + p.Z * M.M33 + M.M43
            );
        }

        /// <summary>
        /// Converte dagli angoli di roll, pitch e yaw (in gradi) in un quaternion
        /// </summary>
        /// <param name="rxDeg"></param>
        /// <param name="ryDeg"></param>
        /// <param name="rzDeg"></param>
        /// <returns></returns>
        public static Quaternion QuaternionFromRPY(double rxDeg, double ryDeg, double rzDeg)
        {
            // Converte gli angoli in radianti
            double rx = Math.PI * rxDeg / 180.0;
            double ry = Math.PI * ryDeg / 180.0;
            double rz = Math.PI * rzDeg / 180.0;

            // Calcola i quaternioni per ogni asse
            Quaternion qx = Quaternion.CreateFromAxisAngle(Vector3.UnitX, (float)rx);
            Quaternion qy = Quaternion.CreateFromAxisAngle(Vector3.UnitY, (float)ry);
            Quaternion qz = Quaternion.CreateFromAxisAngle(Vector3.UnitZ, (float)rz);

            // Rotazioni ZYX (yaw-pitch-roll)
            return qz * qy * qx;
        }

        /// <summary>
        /// Converte un quaternion in angoli di roll, pitch e yaw (in gradi)
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        public static Vector3 RPYFromQuaternion(Quaternion q)
        {
            // Calcola roll
            double sinr_cosp = 2 * (q.W * q.X + q.Y * q.Z);
            double cosr_cosp = 1 - 2 * (q.X * q.X + q.Y * q.Y);
            double roll = Math.Atan2(sinr_cosp, cosr_cosp);

            // Calcola pitch
            double sinp = 2 * (q.W * q.Y - q.Z * q.X);
            double pitch;
            if (Math.Abs(sinp) >= 1)
                pitch = CopySign(Math.PI / 2, sinp); // Gimbal lock
            else
                pitch = Math.Asin(sinp);

            // Calcola yaw
            double siny_cosp = 2 * (q.W * q.Z + q.X * q.Y);
            double cosy_cosp = 1 - 2 * (q.Y * q.Y + q.Z * q.Z);
            double yaw = Math.Atan2(siny_cosp, cosy_cosp);

            // Restituisce gli angoli in gradi
            return new Vector3(
                (float)(roll * 180.0 / Math.PI),
                (float)(pitch * 180.0 / Math.PI),
                (float)(yaw * 180.0 / Math.PI)
            );
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Converte da DescPose a Vector3 x,y,z
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private Vector3 DescPoseTranToVector3(DescPose p)
        {
            return new Vector3((float)p.tran.x, (float)p.tran.y, (float)p.tran.z);
        }

        /// <summary>
        /// Converte da DescPose a Vector3 rx,ry,rz
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private Vector3 DescPoseRpyToVector3(DescPose p)
        {
            return new Vector3((float)p.rpy.rx, (float)p.rpy.ry, (float)p.rpy.rz);
        }

        /// <summary>
        /// Converte da Descpose a angoli di eulero ZYX
        /// </summary>
        /// <param name="pose"></param>
        /// <returns></returns>
        private static Vector3 DescPoseToEulerZYX(DescPose pose)
        {
            // Rx = roll, Ry = pitch, Rz = yaw
            // ATTENZIONE: verifica che questi siano in gradi già, altrimenti converti da radianti
            return new Vector3(
                (float)pose.rpy.rx,
                (float)pose.rpy.ry,
                (float)pose.rpy.rz
            );
        }

        /// <summary>
        /// Normalizza un angolo in gradi tra -180 e 180
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        private static float NormalizeAngle(float angle)
        {
            while (angle > 180) angle -= 360;
            while (angle <= -180) angle += 360;
            return angle;
        }

        /// <summary>
        /// Restituisce la matrice 4x4 sotto forma di stringa
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        private string MatrixToString(Matrix4x4 m) =>
            string.Format(
                "[{0,6:F3} {1,6:F3} {2,6:F3} {3,6:F3}]\n" +
                "[{4,6:F3} {5,6:F3} {6,6:F3} {7,6:F3}]\n" +
                "[{8,6:F3} {9,6:F3} {10,6:F3} {11,6:F3}]\n" +
                "[{12,6:F3} {13,6:F3} {14,6:F3} {15,6:F3}]",
                m.M11, m.M12, m.M13, m.M14,
                m.M21, m.M22, m.M23, m.M24,
                m.M31, m.M32, m.M33, m.M34,
                m.M41, m.M42, m.M43, m.M44
                );

        /// <summary>
        /// Restituisce il valore assoluto di <paramref name="magnitude"/> con il segno di <paramref name="sign"></paramref>
        /// </summary>
        /// <param name="magnitude"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        private static double CopySign(double magnitude, double sign)
        {
            return Math.Sign(sign) >= 0 ? Math.Abs(magnitude) : -Math.Abs(magnitude);
        }

        #endregion

        private void ClickEvent_goToOrigin(object sender, EventArgs e)
        {
            // Punto del robot
            DescPose poseToSend = new DescPose(
                0.0f,       // tran.x
                0.0f,    // tran.y
                0.0f,   // tran.z
                90.0f,       // rpy.rx
                0.0,     // rpy.ry
                178.0f       // rpy.rz
            );

            // Invio punto al robot in move cart
            int err = RobotManager.robot.MoveCart(poseToSend, RobotManager.tool, RobotManager.user, 10, 10,
                                                   RobotManager.ovl, RobotManager.blendT, RobotManager.config);
        }

        private void PnlClose_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void button1_Click_1(object sender, EventArgs e) //verso home
        {
            if(!RobotManager.isInHomePosition)
            {
                if(!RobotManager.isInSafeZone)
                {
                    CustomMessageBox.Show(MessageBoxTypeEnum.WARNING_OK, "Posizionare il robot nella zona sicura");
                    return;
                }
                RobotManager.taskManager.AddAndStartTask(nameof(RobotManager.HomeRoutine), RobotManager.HomeRoutine, TaskType.Default, false);
                button2.Enabled = true;
                progressBar1.Value = 15;
                return;
            }
            button2.Enabled = true;
            progressBar1.Value = 15;
        }

        private void button2_Click(object sender, EventArgs e) //verso origine
        {
            if (!RobotManager.isInHomePosition)
            {
                CustomMessageBox.Show(MessageBoxTypeEnum.WARNING_OK, "Routine di home position non conclusa");
                return;
            }
            JointPos jointStart = new JointPos(0, 0, 0, 0, 0, 0);
            DescPose descPosStart = new DescPose(0, 0, 0, 90.678, 0.858, -179.343);
            RobotManager.robot.GetInverseKin(0, descPosStart, -1, ref jointStart);
            JointPos jointRetract = new JointPos(0, 0, 0, 0, 0, 0);
            DescPose descPosRetract = new DescPose(0, 0, 0, 90.678, 0.858, -179.343);
            descPosRetract.tran.y -= 250;
            RobotManager.robot.GetInverseKin(0, descPosRetract, -1, ref jointRetract);

            ExaxisPos epos = new ExaxisPos(0, 0, 0, 0); // Nessun asse esterno
            DescPose offset = new DescPose(0, 0, 0, 0, 0, 0); // Nessun offset
            byte offsetFlag = 0; // Flag per offset (0 = disabilitato)

            int movementResult = RobotManager.robot.MoveL(jointRetract, descPosRetract, RobotManager.tool, RobotManager.user, RobotManager.vel,
                RobotManager.acc, RobotManager.ovl, RobotManager.blendT, epos, 0, offsetFlag, offset);
            RobotManager.GetRobotMovementCode(movementResult);
            movementResult = RobotManager.robot.MoveL(jointStart, descPosStart, RobotManager.tool, RobotManager.user, RobotManager.vel,
                RobotManager.acc, RobotManager.ovl, RobotManager.blendT, epos, 0, offsetFlag, offset);
            RobotManager.GetRobotMovementCode(movementResult);

            RobotManager.inPosition = false;
            RobotManager.endingPoint = descPosStart;
            button3.Enabled = true;
            progressBar1.Value = 30;
        }

        private void button3_Click(object sender, EventArgs e) //verso x
        {
            if (!RobotManager.inPosition)
            {
                CustomMessageBox.Show(MessageBoxTypeEnum.WARNING_OK, "Movimento precedente non concluso");
                return;
            }

            // lettura della posizione del tracker e del robot
            _trackerPoints[0] = DescPoseTranToVector3(RobotManager.openVRsupport.HTCVIVETrackerPosition);
            _robotPoints[0] = new Vector3(
                  (float)RobotManager.TCPCurrentPosition.tran.x,
                  (float)RobotManager.TCPCurrentPosition.tran.y,
                  (float)RobotManager.TCPCurrentPosition.tran.z
                );
            // lettura della rotazione del tracker e del robot
            _trackerRotations[0] = DescPoseToEulerZYX(RobotManager.openVRsupport.HTCVIVETrackerPosition);
            _robotRotations[0] = new Vector3(
                  (float)RobotManager.TCPCurrentPosition.rpy.rx,
                  (float)RobotManager.TCPCurrentPosition.rpy.ry,
                  (float)RobotManager.TCPCurrentPosition.rpy.rz
                );

            LogMessage($"Origine registrata - punto tracker: {_trackerPoints[0]}, punto Robot: {_robotPoints[0]}");
            LogMessage($"Origine registrata - rot tracker: {_trackerRotations[0]}, rot Robot: {_robotRotations[0]}");

            AdvanceUI(CalibrationStep.XAxis, btnRecordX, 33, "Muovi il tracker sull'ASSE X del robot e clicca 'Registra X'");

            JointPos jointX = new JointPos(0, 0, 0, 0, 0, 0);
            DescPose descPosX = new DescPose(200, 0, 0, 90.678, 0.858, -179.343);
            RobotManager.robot.GetInverseKin(0, descPosX, -1, ref jointX);

            ExaxisPos epos = new ExaxisPos(0, 0, 0, 0); // Nessun asse esterno
            DescPose offset = new DescPose(0, 0, 0, 0, 0, 0); // Nessun offset
            byte offsetFlag = 0; // Flag per offset (0 = disabilitato)

            int movementResult = RobotManager.robot.MoveL(jointX, descPosX, RobotManager.tool, RobotManager.user, RobotManager.vel,
                RobotManager.acc, RobotManager.ovl, RobotManager.blendT, epos, 0, offsetFlag, offset);
            RobotManager.GetRobotMovementCode(movementResult);

            RobotManager.inPosition = false;
            RobotManager.endingPoint = descPosX;

            button4.Enabled = true;
            progressBar1.Value = 45;
        }

        private void button4_Click(object sender, EventArgs e) //verso origine
        {
            if (!RobotManager.inPosition)
            {
                CustomMessageBox.Show(MessageBoxTypeEnum.WARNING_OK, "Movimento precedente non concluso");
                return;
            }

            // lettura della posizione del tracker e del robot
            _trackerPoints[1] = DescPoseTranToVector3(RobotManager.openVRsupport.HTCVIVETrackerPosition);
            _robotPoints[1] = new Vector3(
                  (float)RobotManager.TCPCurrentPosition.tran.x,
                  (float)RobotManager.TCPCurrentPosition.tran.y,
                  (float)RobotManager.TCPCurrentPosition.tran.z
                );
            // lettura della rotazione del tracker e del robot
            _trackerRotations[1] = DescPoseToEulerZYX(RobotManager.openVRsupport.HTCVIVETrackerPosition);
            _robotRotations[1] = new Vector3(
                  (float)RobotManager.TCPCurrentPosition.rpy.rx,
                  (float)RobotManager.TCPCurrentPosition.rpy.ry,
                  (float)RobotManager.TCPCurrentPosition.rpy.rz
                );

            LogMessage($"Punto asse x registrato - punto tracker: {_trackerPoints[1]}, punto Robot: {_robotPoints[1]}");
            LogMessage($"Punto asse x registrato - rot tracker: {_trackerRotations[1]}, rot Robot: {_robotRotations[1]}");

            AdvanceUI(CalibrationStep.ZAxis, btnRecordZ, 66, "Muovi il tracker sull'ASSE Z del robot e clicca 'Registra Z'");

            JointPos jointStart = new JointPos(0, 0, 0, 0, 0, 0);
            DescPose descPosStart = new DescPose(0, 0, 0, 90.678, 0.858, -179.343);
            RobotManager.robot.GetInverseKin(0, descPosStart, -1, ref jointStart);

            ExaxisPos epos = new ExaxisPos(0, 0, 0, 0); // Nessun asse esterno
            DescPose offset = new DescPose(0, 0, 0, 0, 0, 0); // Nessun offset
            byte offsetFlag = 0; // Flag per offset (0 = disabilitato)

            int movementResult = RobotManager.robot.MoveL(jointStart, descPosStart, RobotManager.tool, RobotManager.user, RobotManager.vel,
                RobotManager.acc, RobotManager.ovl, RobotManager.blendT, epos, 0, offsetFlag, offset);
            RobotManager.GetRobotMovementCode(movementResult);

            RobotManager.inPosition = false;
            RobotManager.endingPoint = descPosStart;

            button5.Enabled = true;
            progressBar1.Value = 60;
        }

        private void button5_Click(object sender, EventArgs e) //verso z
        {
            if (!RobotManager.inPosition)
            {
                CustomMessageBox.Show(MessageBoxTypeEnum.WARNING_OK, "Movimento precedente non concluso");
                return;
            }

            JointPos jointZ = new JointPos(0, 0, 0, 0, 0, 0);
            DescPose descPosZ = new DescPose(0, 0, 200, 90.678, 0.858, -179.343);
            RobotManager.robot.GetInverseKin(0, descPosZ, -1, ref jointZ);

            ExaxisPos epos = new ExaxisPos(0, 0, 0, 0); // Nessun asse esterno
            DescPose offset = new DescPose(0, 0, 0, 0, 0, 0); // Nessun offset
            byte offsetFlag = 0; // Flag per offset (0 = disabilitato)

            int movementResult = RobotManager.robot.MoveL(jointZ, descPosZ, RobotManager.tool, RobotManager.user, RobotManager.vel,
                RobotManager.acc, RobotManager.ovl, RobotManager.blendT, epos, 0, offsetFlag, offset);
            RobotManager.GetRobotMovementCode(movementResult);

            RobotManager.inPosition = false;
            RobotManager.endingPoint = descPosZ;

            button6.Enabled = true;
            progressBar1.Value = 75;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!RobotManager.inPosition)
            {
                CustomMessageBox.Show(MessageBoxTypeEnum.WARNING_OK, "Movimento precedente non concluso");
                return;
            }

            // lettura della posizione del tracker e del robot
            _trackerPoints[2] = DescPoseTranToVector3(RobotManager.openVRsupport.HTCVIVETrackerPosition);
            _robotPoints[2] = new Vector3(
                  (float)RobotManager.TCPCurrentPosition.tran.x,
                  (float)RobotManager.TCPCurrentPosition.tran.y,
                  (float)RobotManager.TCPCurrentPosition.tran.z
                );
            // lettura della rotazione del tracker e del robot
            _trackerRotations[2] = DescPoseToEulerZYX(RobotManager.openVRsupport.HTCVIVETrackerPosition);
            _robotRotations[2] = new Vector3(
                  (float)RobotManager.TCPCurrentPosition.rpy.rx,
                  (float)RobotManager.TCPCurrentPosition.rpy.ry,
                  (float)RobotManager.TCPCurrentPosition.rpy.rz
                );

            LogMessage($"Punto asse z registrato - punto tracker: {_trackerPoints[2]}, punto Robot: {_robotPoints[2]}");
            LogMessage($"Punto asse z registrato - rot tracker: {_trackerRotations[2]}, rot Robot: {_robotRotations[2]}");

            AdvanceUI(CalibrationStep.Done, btnConfirmCalibration, 100, "Calibrazione completata! Clicca 'Conferma' per salvare.");

            try
            {
                // 1) Pose snapshot per calibrazione
                DescPose tp = RobotManager.openVRsupport.HTCVIVETrackerPosition;
                DescPose rp = RobotManager.TCPCurrentPosition;

                // 2) Genera matrice 4×4 di allineamento (rotazione + traslazione)
                var M = CalculateAlignmentWithKabsch(_trackerPoints, _robotPoints);
                RobotManager.openVRsupport._trackerToRobotTransformation = M;

                // 3) Estrai solo la rotazione pura e trasformala in quaternion
                //     (la traslazione è in M.M41–M43, non ci serve per l’offset di orientamento)
                Matrix4x4 Rrot = M;
                Rrot.M41 = Rrot.M42 = Rrot.M43 = 0;
                Quaternion qMat = Quaternion.CreateFromRotationMatrix(Rrot);

                // 4) Leggi quaternion di tracker e robot al momento della calibrazione
                Quaternion qTrackerCalib = QuaternionFromRPY(
                    tp.rpy.rx, tp.rpy.ry, tp.rpy.rz);
                Quaternion qRobotCalib = QuaternionFromRPY(
                    rp.rpy.rx, rp.rpy.ry, rp.rpy.rz);

                // 5) Calcola l’offset quaternion in un’unica moltiplicazione:
                //     vogliamo che: qRobotCalib = rotationOffset * (qMat * qTrackerCalib)
                RobotManager.openVRsupport.rotationOffsetQuat =
                    qRobotCalib * Quaternion.Inverse(qMat * qTrackerCalib);

                // Imposta l'ultima posa del tracker trasformata con quella attuale
                lastTrackerPose = GetActualTransformedTrackerPose();

                LogMessage("=== Calibrazione completata ===");
                LogMessage($"Matrice 4×4:\n{MatrixToString(M)}");
                LogMessage($"OffsetQuat:\n{RobotManager.openVRsupport.rotationOffsetQuat}");

                // Imposta la calibrazione avvenuta
                isCalibrated = true;
            }
            catch (Exception ex)
            {
                LogMessage($"ERRORE CALIBRAZIONE: {ex.Message}");
                MessageBox.Show("Calibrazione fallita", "Errore",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                isCalibrated = false;
            }

            ExaxisPos epos = new ExaxisPos(0, 0, 0, 0); // Nessun asse esterno
            DescPose offset = new DescPose(0, 0, 0, 0, 0, 0); // Nessun offset
            byte offsetFlag = 0; // Flag per offset (0 = disabilitato)

            JointPos jointRetract = new JointPos(0, 0, 0, 0, 0, 0);
            DescPose descPosRetract = new DescPose(0, 0, 200, 90.678, 0.858, -179.343);
            descPosRetract.tran.y -= 250;
            RobotManager.robot.GetInverseKin(0, descPosRetract, -1, ref jointRetract);

            int movementResult = RobotManager.robot.MoveL(jointRetract, descPosRetract, RobotManager.tool, RobotManager.user, RobotManager.vel,
                RobotManager.acc, RobotManager.ovl, RobotManager.blendT, epos, 0, offsetFlag, offset);
            RobotManager.GetRobotMovementCode(movementResult);

            RobotManager.taskManager.AddAndStartTask(nameof(RobotManager.HomeRoutine), RobotManager.HomeRoutine, TaskType.Default, false);
            progressBar1.Value = 100;
        }
    }
}


