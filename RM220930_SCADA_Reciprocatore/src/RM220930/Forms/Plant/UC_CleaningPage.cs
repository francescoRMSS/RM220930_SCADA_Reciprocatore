using RMLib.DataAccess;
using RMLib.Logger;
using RMLib.Translations;
using System;
using System.Windows.Forms;

namespace RM.src.RM220930.Forms.Plant
{
    public partial class UC_CleaningPage : UserControl
    {
        #region Proprietà

        #region Variabili per la connessione al DB
        // Variabili per la connessione al database
        private static readonly RobotDAOSqlite robotDAO = new RobotDAOSqlite();
        private static readonly SqliteConnectionConfiguration DatabaseConnection = new SqliteConnectionConfiguration();
        private static readonly string ConnectionString = DatabaseConnection.GetConnectionString();
        #endregion

        /// <summary>
        /// Variabile per usare i messaggi di log
        /// </summary>
        private static readonly log4net.ILog log = LogHelper.GetLogger(); // Logger

        #endregion

        public UC_CleaningPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Chiude la pagina tornando alla homepage, si disiscrive dagli eventi e termina i thread del debug e drag mode
        /// </summary>
        private void CloseForm()
        {
            log.Info("Richiesta chiusura pagina drag/debug mode");
            

            Dispose();
            log.Info("Operazioni di chiusura della pagina drag/debug completate, ri apro la home page");
            FormHomePage.Instance.LabelHeader = TranslationManager.GetTranslation("LBL_HOMEPAGE_HEADER");
            FormHomePage.Instance.PnlContainer.Controls["UC_HomePage"].BringToFront();
            FormHomePage.Instance.PnlContainer.Controls.Remove(Controls["UC_DragMode_Debug"]);
        }

        private void btn_homePage_Click(object sender, EventArgs e)
        {
            CloseForm();
        }
    }
}
