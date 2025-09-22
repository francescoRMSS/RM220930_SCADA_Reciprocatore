using RMLib.MessageBox;
using RMLib.Translations;
using System;
using System.Threading;
using System.Windows.Forms;

namespace RM.src.RM220930.Forms.Plant.ViveTracker
{
    public partial class UC_ViveTrackerPage : UserControl
    {
        public UC_ViveTrackerPage()
        {
            InitializeComponent();
        }

        private void btn_home_Click(object sender, EventArgs e)
        {
            //Dispose();
            FormHomePage.Instance.LabelHeader = TranslationManager.GetTranslation("LBL_HOMEPAGE_HEADER");
            FormHomePage.Instance.PnlContainer.Controls["UC_HomePage"].BringToFront();
            //FormHomePage.Instance.PnlContainer.Controls.Remove(Controls["UC_ViveTrackerPage"]);
        }

        /// <summary>
        /// Aggiorna label fase Robot
        /// </summary>
        /// <param name="description">Descrizione fase</param>
        public void UpdateLbl_coord(float x, float y, float z, float rx, float ry, float rz)
        {
            lbl_xVal.Text = x.ToString();
            lbl_yVal.Text = y.ToString();
            lbl_zVal.Text = z.ToString();
            lbl_rxVal.Text = rx.ToString();
            lbl_ryVal.Text = ry.ToString();
            lbl_rzVal.Text = rz.ToString();
        }

        #region Struttura thread lettura coordinate
        private static Thread trackerPoseThread;
        private static int trackerPoseRefreshPeriod = 100;
        #endregion

        private void ClickEvent_startLiveMovement(object sender, EventArgs e)
        {
            // Inizializzo posizione del tracker
            OpenVRsupport.RobotPoseLive = RobotManager.TCPCurrentPosition;
            

            // Avvio del live motion
            OpenVRsupport.startLiveMotion = true;
            pnl_liveStatus.BackColor = System.Drawing.Color.LimeGreen;
        }

        private void ClickEvent_stopLiveMovement(object sender, EventArgs e)
        {
            OpenVRsupport.startLiveMotion = false;
            pnl_liveStatus.BackColor = System.Drawing.Color.Firebrick;
        }

        private void ClickEvent_startRecord(object sender, EventArgs e)
        {
            /*
            if (OpenVRsupport.startLiveMotion) // start movement
            {
                CustomMessageBox.Show(MessageBoxTypeEnum.WARNING_OK, "Impossibile avviare la registrazione dei punti durante il movimento del robot");
                return;
            }
            else
            {
                // start movement
                pnl_recStatus.BackColor = System.Drawing.Color.LimeGreen;
            }*/
        }

        private void ClickEvent_stopRecord(object sender, EventArgs e)
        {
            // stop record
            pnl_recStatus.BackColor = System.Drawing.Color.Firebrick;
        }

        private void ClickEvent_startMovement(object sender, EventArgs e)
        {
            /*
            if(OpenVRsupport.startLiveMotion)
            {
                CustomMessageBox.Show(MessageBoxTypeEnum.WARNING_OK, "Impossibile avviare il movimento durante il movimento live");
                return;
            }
            else
            {
                // start movement
                pnl_movementStatus.BackColor = System.Drawing.Color.LimeGreen;
            }*/
        }

        private void ClickEventStopMovement(object sender, EventArgs e)
        {
            // stop movement
            pnl_movementStatus.BackColor = System.Drawing.Color.Firebrick;
        }
    }
}
