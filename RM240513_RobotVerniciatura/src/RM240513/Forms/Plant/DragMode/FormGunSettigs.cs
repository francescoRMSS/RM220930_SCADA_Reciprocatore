using RMLib.DataAccess;
using RMLib.Keyboards;
using RMLib.MessageBox;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace RM.src.RM240513.Forms.Plant.DragMode
{
    /// <summary>
    /// Form contenente i controlli per visualizzare e modificare i parametri della pistola e delle relative valvole
    /// proporzionali cp2/3. 
    /// <br></br>
    /// Strutturata in modo che sia possibile selezionare i punti da modificare e applicare le modifiche
    /// </summary>
    public partial class FormGunSettigs : Form
    {
        #region Costanti

        /// <summary>
        /// Valore minimo in BAR per le valvole
        /// </summary>
        private const int PRESSURE_MIN_VALUE = 0;
        /// <summary>
        /// Valore massimo in BAR per le valvole
        /// </summary>
        private const int PRESSURE_MAX_VALUE = 6;
        /// <summary>
        /// Valore minimo in mA per uscita pistola
        /// </summary>
        private const int MICRO_AMPERE_MIN_VALUE = 0;
        /// <summary>
        /// Valore massimo in mA per uscita pistola
        /// </summary>
        private const int MICRO_AMPERE_MAX_VALUE = 100;
        /// <summary>
        /// Valore minimo in kV per uscita pistola
        /// </summary>
        private const int KV_MIN_VALUE = 0;
        /// <summary>
        /// Valore massimo in kV per uscita pistola
        /// </summary>
        private const int KV_MAX_VALUE = 100;

        #endregion

        #region Proprietà

        #region Variabili per la connessione al DB
        // Variabili per la connessione al database
        private static readonly RobotDAOSqlite robotDAO = new RobotDAOSqlite();
        private static readonly SqliteConnectionConfiguration DatabaseConnection = new SqliteConnectionConfiguration();
        private static readonly string ConnectionString = DatabaseConnection.GetConnectionString();
        #endregion

        /// <summary>
        /// Indica se la form è già stata visualizzata a schermo in precedenza
        /// </summary>
        public bool isShown = false;

        /// <summary>
        /// Indica l'indice iniziale scelto per la modifica multipla
        /// </summary>
        public int initialPositionIndex = 0;

        /// <summary>
        /// Indica l'indice finale scelto per la modifica multipla
        /// </summary>
        public int finalPositionIndex = 0;

        /// <summary>
        /// Lista delle posizioni passata come riferimento dalla form drag mode
        /// </summary>
        private readonly ListView lw_positions;

        #endregion

        #region Eventi form

        /// <summary>
        /// Invocato quando il valore dell'indice iniziale viene cambiato
        /// </summary>
        public event EventHandler InitialPointIndexChanged;

        /// <summary>
        /// Invocato quando il valore dell'indice finale viene cambiato
        /// </summary>
        public event EventHandler FinalPointIndexChanged;

        /// <summary>
        /// Invocato quando un valore di gun settings è stato modificato per gli indici selezionati
        /// </summary>
        public event EventHandler GunSettingsUpdated;

        #endregion

        /// <summary>
        /// Costruisce la form in modo automatico e default
        /// </summary>
        public FormGunSettigs(ListView lw_positions)
        {
            InitializeComponent();
            this.lw_positions = lw_positions;
        }

        #region Metodi UI

        /// <summary>
        /// Toglie tutte le scritte dai controlli del panel per le modifiche dei gun settings
        /// </summary>
        public void ResetIndexesLabels()
        {
            initialPositionIndex = 0;
            finalPositionIndex = 0;

            lbl_selectedFrom.Text = string.Empty;
            lbl_selectedTo.Text = string.Empty;  

            lbl_valValve1Mod.Text = string.Empty;
            lbl_valValve2Mod.Text = string.Empty;
            lbl_valValve3Mod.Text = string.Empty;

            lbl_valAMod.Text = string.Empty;
            lbl_valVMod.Text = string.Empty;

            btn_disableStatus.BackColor = SystemColors.Control;
            btn_enableStatus.BackColor = SystemColors.Control;
        }

        /// <summary>
        /// Toglie tutte le scritte dai controlli del panel per le modifiche dei gun settings
        /// </summary>
        public void ResetModifyLabels()
        {
            lbl_valValve1Mod.Text = string.Empty;
            lbl_valValve2Mod.Text = string.Empty;
            lbl_valValve3Mod.Text = string.Empty;

            lbl_valAMod.Text = string.Empty;
            lbl_valVMod.Text = string.Empty;

            btn_disableStatus.BackColor = SystemColors.Control;
            btn_enableStatus.BackColor = SystemColors.Control;
        }

        /// <summary>
        /// Restituisce true quando gli indici sono entrambi settati
        /// </summary>
        /// <returns></returns>
        private bool AreIndexesSet()
        {
            if (string.IsNullOrEmpty(lbl_selectedFrom.Text) || string.IsNullOrEmpty(lbl_selectedTo.Text))
            {
                CustomMessageBox.Show(MessageBoxTypeEnum.WARNING_OK, "Inserire prima gli indici degli elementi da modificare");
                return false;
            }
            else
                return true;
        }

        #endregion

        #region Eventi form

        /// <summary>
        /// Evento al click che apre la tastiera e imposta l'indice iniziale della selezione
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_selectFrom(object sender, EventArgs e)
        {
            if (lw_positions.Items.Count < 1)
            {
                CustomMessageBox.Show(MessageBoxTypeEnum.ERROR, "L'applicazione non ha punti da modificare");
                return;
            }
            else if (RobotManager.positionsToSave.Count > 0)
            {
                CustomMessageBox.Show(MessageBoxTypeEnum.ERROR, "L'applicazione ha dei punti ancora da salvare");
                return;
            }

            string newInitialPos = VK_Manager.OpenIntVK("0", 1, lw_positions.Items.Count);
            if (newInitialPos.Equals(VK_Manager.CANCEL_STRING)) return;
            
            int pos = Convert.ToInt32(newInitialPos);

            if(pos < 1)
            {
                CustomMessageBox.Show(MessageBoxTypeEnum.ERROR, "Impossibile selezionare un indice minore di 1");
                return;
            }

            initialPositionIndex = pos;

            lbl_selectedFrom.Text = pos.ToString();

            InitialPointIndexChanged?.Invoke(sender, EventArgs.Empty);
        }

        /// <summary>
        /// Evento al click che apre la tastiera e imposta l'indice finale della selezione
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_selectTo(object sender, EventArgs e)
        {
            if (lw_positions.Items.Count < 1)
            {
                CustomMessageBox.Show(MessageBoxTypeEnum.ERROR, "L'applicazione non ha punti da modificare");
                return;
            }
            else if (RobotManager.positionsToSave.Count > 0)
            {
                CustomMessageBox.Show(MessageBoxTypeEnum.ERROR, "L'applicazione ha dei punti ancora da salvare");
                return;
            }

            string newFinalPos = VK_Manager.OpenIntVK("0", 1, lw_positions.Items.Count);
            if (newFinalPos.Equals(VK_Manager.CANCEL_STRING)) return;

            int pos = Convert.ToInt32(newFinalPos);

            if (pos < 1)
            {
                CustomMessageBox.Show(MessageBoxTypeEnum.ERROR, "Impossibile selezionare un indice minore di 1");
                return;
            }

            finalPositionIndex = pos;

            lbl_selectedTo.Text = pos.ToString();

            FinalPointIndexChanged?.Invoke(sender, EventArgs.Empty);
        }

        /// <summary>
        /// Evento al click che apre la tastiera e imposta il valore in pressione della valvola 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_valveFeedValue(object sender, EventArgs e)
        {
            if (!AreIndexesSet())
                return;

            string newFeedVal = VK_Manager.OpenFloatVK("0", PRESSURE_MIN_VALUE, PRESSURE_MAX_VALUE);
            if (newFeedVal.Equals(VK_Manager.CANCEL_STRING)) return;

            float val = float.Parse(newFeedVal);

            lbl_valValve1Mod.Text = newFeedVal.ToString();

            robotDAO.UpdatePositionFeedValue(ConnectionString, RobotManager.applicationName, initialPositionIndex, finalPositionIndex, val * 100);

            GunSettingsUpdated?.Invoke(sender, EventArgs.Empty);
        }

        /// <summary>
        /// Evento al click che apre la tastiera e imposta il valore in pressione della valvola 2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_valveDosageValue(object sender, EventArgs e)
        {
            if (!AreIndexesSet())
                return;

            string newDosageVal = VK_Manager.OpenFloatVK("0", PRESSURE_MIN_VALUE, PRESSURE_MAX_VALUE);
            if (newDosageVal.Equals(VK_Manager.CANCEL_STRING)) return;

            float val = float.Parse(newDosageVal);

            lbl_valValve2Mod.Text = newDosageVal.ToString();

            robotDAO.UpdatePositionDosageValue(ConnectionString, RobotManager.applicationName, initialPositionIndex, finalPositionIndex, val * 100);

            GunSettingsUpdated?.Invoke(sender, EventArgs.Empty);
        }

        /// <summary>
        /// Evento al click che apre la tastiera e imposta il valore in pressione della valvola 3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_valveGunAirValue(object sender, EventArgs e)
        {
            if (!AreIndexesSet())
                return;

            string newGunAirVal = VK_Manager.OpenFloatVK("0", PRESSURE_MIN_VALUE, PRESSURE_MAX_VALUE);
            if (newGunAirVal.Equals(VK_Manager.CANCEL_STRING)) return;

            float val = float.Parse(newGunAirVal);

            lbl_valValve3Mod.Text = newGunAirVal.ToString();

            robotDAO.UpdatePositionGunAirValue(ConnectionString, RobotManager.applicationName, initialPositionIndex, finalPositionIndex, val * 100);

            GunSettingsUpdated?.Invoke(sender, EventArgs.Empty);
        }

        /// <summary>
        /// Evento al click che apre la tastiera e imposta il valore di uscita mA
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_microAmpereValue(object sender, EventArgs e)
        {
            if (!AreIndexesSet())
                return;

            string newMicroAmpereVal = VK_Manager.OpenFloatVK("0", MICRO_AMPERE_MIN_VALUE, MICRO_AMPERE_MAX_VALUE);
            if (newMicroAmpereVal.Equals(VK_Manager.CANCEL_STRING)) return;

            float val = float.Parse(newMicroAmpereVal);

            lbl_valAMod.Text = newMicroAmpereVal.ToString();

            robotDAO.UpdatePositionMicroAmpValue(ConnectionString, RobotManager.applicationName, initialPositionIndex, finalPositionIndex, val * 100);

            GunSettingsUpdated?.Invoke(sender, EventArgs.Empty);
        }

        /// <summary>
        /// Evento al click che apre la tastiera e imposta il valore di uscita kV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_kiloVoltValue(object sender, EventArgs e)
        {
            if (!AreIndexesSet())
                return;

            string newKiloVoltVal = VK_Manager.OpenFloatVK("0", KV_MIN_VALUE, KV_MAX_VALUE);
            if (newKiloVoltVal.Equals(VK_Manager.CANCEL_STRING)) return;

            float val = float.Parse(newKiloVoltVal);

            lbl_valVMod.Text = newKiloVoltVal.ToString();

            robotDAO.UpdatePositionKiloVoltValue(ConnectionString, RobotManager.applicationName, initialPositionIndex, finalPositionIndex, val * 100);

            GunSettingsUpdated?.Invoke(sender, EventArgs.Empty);
        }

        /// <summary>
        /// Evento al click che apre la tastiera e imposta l'abilitazione della scheda a OFF
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_disableStatus(object sender, EventArgs e)
        {
            if (!AreIndexesSet())
                return;

            btn_enableStatus.BackColor = SystemColors.Control;

            btn_disableStatus.BackColor = Color.Red;

            robotDAO.UpdatePositionStatusValue(ConnectionString, RobotManager.applicationName, initialPositionIndex, finalPositionIndex, false);

            GunSettingsUpdated?.Invoke(sender, EventArgs.Empty);
        }

        /// <summary>
        /// Evento al click che apre la tastiera e imposta l'abilitazione della scheda a ON
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_enableStatus(object sender, EventArgs e)
        {
            if (!AreIndexesSet())
                return;

            btn_disableStatus.BackColor = SystemColors.Control;

            btn_enableStatus.BackColor = Color.Green;

            robotDAO.UpdatePositionStatusValue(ConnectionString, RobotManager.applicationName, initialPositionIndex, finalPositionIndex, true);

            GunSettingsUpdated?.Invoke(sender, EventArgs.Empty);
        }

        #endregion

        #region Eventi per movimento form

        // Variabili per il trascinamento
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        /// <summary>
        /// Spostamento del mouse per il trascinamento della form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseMoveEvent_moving(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        /// <summary>
        /// Click sulla form per il trascinamento in mouse up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseUpEvent_stopMove(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        /// <summary>
        /// Click sulla form per il trascinamento in mouse down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseDownEvent_startMove(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = Location;
        }

        #endregion
    }
}
