using RM.Properties;
using RM.src.RM240513.Classes.PLC;
using RM.src.RM240513.Forms.ScreenSaver;
using RMLib.DataAccess;
using RMLib.Keyboards;
using RMLib.MessageBox;
using RMLib.PLC;
using RMLib.Security;
using RMLib.Translations;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RM.src.RM240513.Forms.Plant
{
    /// <summary>
    /// Contiene i parametri di movimento del Robot
    /// </summary>
    public partial class UC_parameters : UserControl
    {
        #region Variaibli per connessione database
        private static readonly RobotDAOSqlite robotDAO = new RobotDAOSqlite();
        private static readonly SqliteConnectionConfiguration DatabaseConnection = new SqliteConnectionConfiguration();
        private static readonly string ConnectionString = DatabaseConnection.GetConnectionString();
        #endregion

        /// <summary>
        /// Costruttore
        /// </summary>
        public UC_parameters()
        {
            InitializeComponent();

            // Intestazione UC_parameters
            FormHomePage.Instance.LabelHeader = "PARAMETRI ROBOT";

            InitRobotParameters();

            // Avvia il caricamento asincrono dei parametri
            Task.Run(() => InitParameters());

            // Collegamento evento ValueChanged del dizionario al metodo RefreshVariables
            PLCConfig.appVariables.ValueChanged += RefreshVariables;

            ScreenSaverManager.AutoAddClickEvents(this);
        }

        #region Metodi di UC_parameters

        /// <summary>
        /// Inizializza i valori dei controlli presenti nell'interfaccia
        /// </summary>
        private async Task InitParameters()
        {
            // Get del valore dal PLC
            object chain_Enable = PLCConfig.appVariables.getValue(PLCTagName.Chain_Enable);

            // Aggiorna la UI nel thread della UI
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    UpdateUI(chain_Enable);
                }));
            }
            else
            {
                UpdateUI(chain_Enable);
            }

        }

        /// <summary>
        /// Aggiorna interfaccia dopo aver eseguito get dei valori dal PLC
        /// </summary>
        /// <param name="chain_Enable">Abilitazione catena</param>
        private void UpdateUI(object chain_Enable)
        {
            if (Convert.ToInt16(chain_Enable) == 1)
            {
                btn_chainEnable.BackColor = Color.LimeGreen;
                btn_chainEnable.Text = "Chain ON";
            }
            else
            {
                btn_chainEnable.BackColor = Color.Red;
                btn_chainEnable.Text = "Chain OFF";
            }

        }

        /// <summary>
        /// Metodo richiamato dall'evento ValueChanged del dizionario delle variabili PLC
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RefreshVariables(object sender, DictionaryChangedEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<object, DictionaryChangedEventArgs>(RefreshVariables), sender, e);
                return;
            }

            switch (e.Key)
            {
                case PLCTagName.Chain_Enable:
                    if (Convert.ToInt16(e.NewValue) == 1)
                    {
                        btn_chainEnable.BackColor = Color.LimeGreen;
                        btn_chainEnable.Text = "Chain ON";
                    }
                    else
                    {
                        btn_chainEnable.BackColor = Color.Red;
                        btn_chainEnable.Text = "Chain OFF";
                    }
                    break;
            }
        }

        /// <summary>
        /// Mostra una schermata di caricamento per tot tempo
        /// </summary>
        public void ShowLoadingScreen()
        {

        }

        /// <summary>
        /// Inizializza i tb
        /// </summary>
        private void InitRobotParameters()
        {
            if (RobotManager.robotProperties != null)
            {
                lbl_tool.Text = RobotManager.robotProperties.Tool.ToString();
                lbl_user.Text = RobotManager.robotProperties.User.ToString();
                lbl_blend.Text = RobotManager.robotProperties.Blend.ToString();
                lbl_velocity.Text = RobotManager.robotProperties.Velocity.ToString();
                lbl_acceleration.Text = RobotManager.robotProperties.Acceleration.ToString();
                lbl_ovl.Text = RobotManager.robotProperties.Ovl.ToString();
                lbl_weight.Text = RobotManager.robotProperties.Weight.ToString();
                lbl_freq.Text = RobotManager.robotProperties.VelRec.ToString();
            }
            else
            {
                lbl_tool.Text = "err";
                lbl_user.Text = "err";
                lbl_blend.Text = "err";
                lbl_velocity.Text = "err";
                lbl_acceleration.Text = "err";
                lbl_ovl.Text = "err";
                lbl_freq.Text = "err";
                lbl_weight.Text = "err";
            }
        }

        #endregion

        #region Eventi di UC_parameters

        /// <summary>
        /// Ritorno a HomePage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnHome_Click(object sender, EventArgs e)
        {
            Dispose();
            FormHomePage.Instance.LabelHeader = TranslationManager.GetTranslation("LBL_HOMEPAGE_HEADER");
            FormHomePage.Instance.PnlContainer.Controls["UC_HomePage"].BringToFront();
            FormHomePage.Instance.PnlContainer.Controls.Remove(Controls["UC_applications"]);
        }

        // Info panels

        /// <summary>
        /// Info su Tool
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PnlInfoTool_Click(object sender, EventArgs e)
        {
            FormInfoRobot form = new FormInfoRobot("Tool",
               "Oggetto utilizzato dal robot per eseguire specifici compiti");
            form.ShowDialog();
        }

        /// <summary>
        /// Info su User
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PnlInfoUser_Click(object sender, EventArgs e)
        {
            FormInfoRobot form = new FormInfoRobot("User",
              "Utente utilizzato per interagire con il robot");
            form.ShowDialog();
        }

        /// <summary>
        /// Info su Blend
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PnlInfoBlend_Click(object sender, EventArgs e)
        {
            FormInfoRobot form = new FormInfoRobot("Blend [ms]",
             "Tempo, espresso in millisecondi, che il robot impiega per passare da una posizione all'altra, " +
             "determinandone la fluidità e la precisione del movimento");
            form.ShowDialog();
        }

        /// <summary>
        /// Info su Velocity
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PnlInfoVelocity_Click(object sender, EventArgs e)
        {
            FormInfoRobot form = new FormInfoRobot("Velocity [%]",
            "Velocità di movimento del robot, espressa in percentuale rispetto alla velocità massima possibile");
            form.ShowDialog();
        }

        /// <summary>
        /// Info su Acceleration
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PnlInfoAcceleration_Click(object sender, EventArgs e)
        {
            FormInfoRobot form = new FormInfoRobot("Acceleration",
           "Rapidità con cui il robot può aumentare la sua velocità");
            form.ShowDialog();
        }

        /// <summary>
        /// Info su Ovl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PnlInfoOvl_Click(object sender, EventArgs e)
        {
            FormInfoRobot form = new FormInfoRobot("Ovl",
           "Fattore di scala che consente di regolare dinamicamente la velocità di esecuzione del robot");
            form.ShowDialog();
        }

        /// <summary>
        /// Info su weight
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_weightInfo(object sender, EventArgs e)
        {
            FormInfoRobot form = new FormInfoRobot("Weight",
            "Carico del robot in kg. In drag mode applica la forza necessaria per sorreggere tale peso.");
            form.ShowDialog();
        }

        /// <summary>
        /// Info su frequenza di campionamento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_frequencyInfo(object sender, EventArgs e)
        {
            FormInfoRobot form = new FormInfoRobot("Sampling frequency",
            "Frequenza di campionamento usata per la drag mode lineare. Specifica ogni quanti ms si ottengono le coordinate del robot.");
            form.ShowDialog();
        }

        // Parameters text boxes

        /// <summary>
        /// Aggiornamento Tool
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LblTool_Click(object sender, EventArgs e)
        {
            if (!SecurityManager.ActionRequestCheck("rmAction")) return;

            string newTool = VK_Manager.OpenIntVK("0", 0, 14);
            if (newTool.Equals(VK_Manager.CANCEL_STRING)) return;
            if (Convert.ToInt16(newTool) < 0 || Convert.ToInt16(newTool) > 14)
            {
                CustomMessageBox.Show(MessageBoxTypeEnum.ERROR, "Selezionare un valore da 0 a 14");
                return;
            }
            else
            {
                //controllo che robot manager sia istanziato
                if (RobotManager.robotProperties != null)
                {
                    robotDAO.SetRobotTool(ConnectionString, Convert.ToInt16(newTool));
                    lbl_tool.Text = newTool;
                }
                else
                {
                    lbl_tool.Text = "err";
                }
            }
        }

        /// <summary>
        /// Aggiornamento User
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LblUser_Click(object sender, EventArgs e)
        {
            if (!SecurityManager.ActionRequestCheck("rmAction")) return;

            string newUser = VK_Manager.OpenIntVK("0", 1, 14);
            if (newUser.Equals(VK_Manager.CANCEL_STRING)) return;
            if (Convert.ToInt16(newUser) < 1 || Convert.ToInt16(newUser) > 14)
            {
                CustomMessageBox.Show(MessageBoxTypeEnum.ERROR, "Selezionare un valore da 1 a 14");
                return;
            }
            else
            {
                //controllo che robot manager sia istanziato
                if (RobotManager.robotProperties != null)
                {
                    robotDAO.SetRobotUser(ConnectionString, Convert.ToInt16(newUser));
                    lbl_user.Text = newUser;
                }
                else
                {
                    lbl_user.Text = "err";
                }
            }
        }

        /// <summary>
        /// Aggiornamento Blend
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LblBlend_Click(object sender, EventArgs e)
        {
            if (!SecurityManager.ActionRequestCheck("rmAction")) return;

            string newBlend = VK_Manager.OpenIntVK("0", 1, 500);
            if (newBlend.Equals(VK_Manager.CANCEL_STRING)) return;
            if (Convert.ToInt16(newBlend) < 1 || Convert.ToInt16(newBlend) > 500)
            {
                CustomMessageBox.Show(MessageBoxTypeEnum.ERROR, "Selezionare un valore da 1 a 500");
                return;
            }
            else
            {
                //controllo che robot manager sia istanziato
                if (RobotManager.robotProperties != null)
                {
                    robotDAO.SetRobotBlend(ConnectionString, Convert.ToInt16(newBlend));
                    lbl_blend.Text = newBlend;
                }
                else
                {
                    lbl_blend.Text = "err";
                }
            }
        }

        /// <summary>
        /// Aggiornamento Velocity
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LblVelocity_Click(object sender, EventArgs e)
        {
            if (!SecurityManager.ActionRequestCheck("rmAction")) return;

            string newVelocity = VK_Manager.OpenIntVK("0", 1, 100);
            if (newVelocity.Equals(VK_Manager.CANCEL_STRING)) return;
            if (Convert.ToInt16(newVelocity) < 1 || Convert.ToInt16(newVelocity) > 100)
            {
                CustomMessageBox.Show(MessageBoxTypeEnum.ERROR, "Selezionare un valore da 1 a 100");
                return;
            }
            else
            {
                //controllo che robot manager sia istanziato
                if (RobotManager.robotProperties != null)
                {
                    robotDAO.SetRobotVelocity(ConnectionString, Convert.ToInt16(newVelocity));

                    // Invoco evento per segnalare alla home page di cambiare la label contenente il valore della velocità
                    RobotManager.TriggerRobotVelocityChangedEvent(Convert.ToInt16(newVelocity));

                    lbl_velocity.Text = newVelocity;
                }
                else
                {
                    lbl_velocity.Text = "err";
                }
            }
        }

        /// <summary>
        /// Aggiornamento Acceleration
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LblAcceleration_Click(object sender, EventArgs e)
        {
            if (!SecurityManager.ActionRequestCheck("rmAction")) return;

            string newAcceleration = VK_Manager.OpenIntVK("0", 1, 100);
            if (newAcceleration.Equals(VK_Manager.CANCEL_STRING)) return;
            if (Convert.ToInt16(newAcceleration) < 1 || Convert.ToInt16(newAcceleration) > 100)
            {
                CustomMessageBox.Show(MessageBoxTypeEnum.ERROR, "Selezionare un valore da 1 a 100");
                return;
            }
            else
            {
                //controllo che robot manager sia istanziato
                if (RobotManager.robotProperties != null)
                {
                    robotDAO.SetRobotAcceleration(ConnectionString, Convert.ToInt16(newAcceleration));
                    lbl_acceleration.Text = newAcceleration;
                }
                else
                {
                    lbl_acceleration.Text = "err";
                }
            }
        }

        /// <summary>
        /// Aggiornamento Ovl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LblOvl_Click_1(object sender, EventArgs e)
        {
            if (!SecurityManager.ActionRequestCheck("rmAction")) return;

            string newOvl = VK_Manager.OpenIntVK("0", 1, 100);
            if (newOvl.Equals(VK_Manager.CANCEL_STRING)) return;
            if (Convert.ToInt16(newOvl) < 1 || Convert.ToInt16(newOvl) > 100)
            {
                CustomMessageBox.Show(MessageBoxTypeEnum.ERROR, "Selezionare un valore da 1 a 100");
                return;
            }
            else
            {
                //controllo che robot manager sia istanziato
                if (RobotManager.robotProperties != null)
                {
                    robotDAO.SetRobotOvl(ConnectionString, Convert.ToInt16(newOvl));
                    lbl_ovl.Text = newOvl;
                }
                else
                {
                    lbl_acceleration.Text = "err";
                }
            }
        }

        /// <summary>
        /// Impostazione del carico del robot
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_weightLabel(object sender, EventArgs e)
        {
            if (!SecurityManager.ActionRequestCheck("rmAction")) return;

            string newWeight = VK_Manager.OpenIntVK("0");
            if (newWeight.Equals(VK_Manager.CANCEL_STRING)) return;
            if (Convert.ToInt16(newWeight) < 1 || Convert.ToInt16(newWeight) > 20)
            {
                CustomMessageBox.Show(MessageBoxTypeEnum.ERROR, "Selezionare un valore da 1 a 20");
                return;
            }
            else
            {
                robotDAO.SetRobotWeight(ConnectionString, Convert.ToInt16(newWeight));
                RobotManager.robot.SetLoadWeight(float.Parse(newWeight));
                lbl_weight.Text = newWeight;
            }
        }

        /// <summary>
        /// Esegue setting frequency
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_freqLabel(object sender, EventArgs e)
        {
            if (!SecurityManager.ActionRequestCheck("rmAction")) return;

            string newFreq = VK_Manager.OpenIntVK("0");
            if (newFreq.Equals(VK_Manager.CANCEL_STRING)) return;
            if (Convert.ToInt16(newFreq) < 100 || Convert.ToInt16(newFreq) > 1000)
            {
                CustomMessageBox.Show(MessageBoxTypeEnum.ERROR, "Selezionare un valore da 1 a 1000");
                return;
            }
            else
            {
                robotDAO.SetRobotVelRec(ConnectionString, Convert.ToInt16(newFreq));
                lbl_freq.Text = newFreq;
            }
        }

        #endregion

        private void btn_chainEnable_Click(object sender, EventArgs e)
        {
            object chain_Enable = PLCConfig.appVariables.getValue(PLCTagName.Chain_Enable);

            if (Convert.ToInt16(chain_Enable) == 1)
            {
                RefresherTask.AddUpdate(PLCTagName.Chain_Enable, 0, "INT16");
            }
            else
            {
                RefresherTask.AddUpdate(PLCTagName.Chain_Enable, 1, "INT16");
            }
        }

    }
}
