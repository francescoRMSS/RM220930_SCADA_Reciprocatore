using RM.Properties;
using RM.src.RM220930.Classes.Navigator;
using RMLib.Alarms;
using RMLib.MessageBox;
using RMLib.Security;
using RMLib.VATView;
using RMLib.View;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace RM.src.RM220930.Forms.Plant
{
    /// <summary>
    /// Pagina di home
    /// </summary>
    public partial class UC_HomePage : UserControl, INavigable, INavigationRequester
    {
        #region Events

        /// <summary>
        /// Evento che intercetta il navigator quando è stata richiesta una navigazione tra le UC
        /// </summary>
        public event EventHandler<NavigateEventArgs> NavigateRequested;

        #endregion

        
        #region Graphic interface
        /*
        #region pnl_axis

        #region Custom button

        /// <summary>
        /// Dimensione lato x
        /// </summary>
        int xSize = 55;

        /// <summary>
        /// Dimensione lato y
        /// </summary>
        int ySize = 55;

        /// <summary>
        /// Offset su asse x dei button Z
        /// </summary>
        int xOffsetZButton = 75;

        /// <summary>
        /// Button z1
        /// </summary>
        private CustomButton customBtn_z1;

        /// <summary>
        /// Button OFF z1
        /// </summary>
        private CustomButton customBtn_OFFz1;

        /// <summary>
        /// Button z2
        /// </summary>
        private CustomButton customBtn_z2;

        /// <summary>
        /// Button OFF z2
        /// </summary>
        private CustomButton customBtn_OFFz2;

        /// <summary>
        /// Button z3
        /// </summary>
        private CustomButton customBtn_z3;

        /// <summary>
        /// Button OFF z3
        /// </summary>
        private CustomButton customBtn_OFFz3;

        /// <summary>
        /// Button z4
        /// </summary>
        private CustomButton customBtn_z4;

        /// <summary>
        /// Button OFF z4
        /// </summary>
        private CustomButton customBtn_OFFz4;

        /// <summary>
        /// Button z5
        /// </summary>
        private CustomButton customBtn_z5;

        /// <summary>
        /// Button OFF z5
        /// </summary>
        private CustomButton customBtn_OFFz5;

        /// <summary>
        /// Button z6
        /// </summary>
        private CustomButton customBtn_z6;

        /// <summary>
        /// Button OFF z6
        /// </summary>
        private CustomButton customBtn_OFFz6;

        /// <summary>
        /// Button z7
        /// </summary>
        private CustomButton customBtn_z7;

        /// <summary>
        /// Button OFF z7
        /// </summary>
        private CustomButton customBtn_OFFz7;

        /// <summary>
        /// Button z8
        /// </summary>
        private CustomButton customBtn_z8;

        /// <summary>
        /// Button OFF z8
        /// </summary>
        private CustomButton customBtn_OFFz8;

        #endregion

        #endregion

        #region pnl_options

        #region Custom button

        /// <summary>
        /// Button laser
        /// </summary>
        private CustomButton customBtn_laser;

        /// <summary>
        /// Button 3D
        /// </summary>
        private CustomButton customBtn_3D;

        /// <summary>
        /// Button lightd
        /// </summary>
        private CustomButton customBtn_lights;

        #endregion

        #region Label

        /// <summary>
        /// Label LASER
        /// </summary>
        private Label lb_laser;

        /// <summary>
        /// Label LIVE 3D
        /// </summary>
        private Label lb_3D;

        /// <summary>
        /// Label LUCI CABINA
        /// </summary>
        private Label lb_lights;

        #endregion

        #endregion

        #region pnl_hub

        #region Custom button

        /// <summary>
        /// Button up
        /// </summary>
        private CustomButton customBtn_up;

        /// <summary>
        /// Button down
        /// </summary>
        private CustomButton customBtn_down;

        /// <summary>
        /// Button hubOFF
        /// </summary>
        private CustomButton customBtn_hubOFF;

        #endregion

        #region Custom panel

        CustomPanel customPnl_valHub;

        #endregion

        #region Label

        /// <summary>
        /// Label ValHub
        /// </summary>
        private Label lb_valHub;

        #endregion

        #endregion
        */
        #endregion
        
        /// <summary>
        /// Costruttore
        /// </summary>
        public UC_HomePage()
        {
            InitializeComponent();
            
        }

        #region Metodi di UC_HomePage

        /// <summary>
        /// Metodo eseguito dopo la richiesta di navigazione in home page
        /// </summary>
        /// <param name="parameter"></param>
        public void OnNavigatedTo(object parameter)
        {
            // Se il parametro è una stringa con il titolo
            if (parameter is string pageTitle)
            {
                lbl_title.Text = pageTitle;
            }

        }

        /// <summary>
        /// Crea dinamicamente tutti i controlli presenti all'interno di pnl_axis
        /// </summary>
        private void InitDynamicControlsAxis()
        {
            /*
            CreateZ1Button();
            CreateZ2Button();
            CreateZ3Button();
            CreateZ4Button();
            CreateZ5Button();
            CreateZ6Button();
            CreateZ7Button();
            CreateZ8Button();
            */
        }

        /// <summary>
        /// Crea dinamicamente tutti i controlli presenti all'interno di pnl_options
        /// </summary>
        private void InitDynamicControlsOptions()
        {
            /*
            CreateLaserButton();
            Create3DButton();
            CreateLightsButton();
            */
        }

        /// <summary>
        /// Crea dinamicamente tutti i controlli presenti all'interno di pnl_hub
        /// </summary>
        private void InitDynamicControlsHub()
        {
            /*
            CreateUpButton();
            CreateDownButton();
            CreateValHubPanelLabel();
            CreateHubOFFButton();
            */
        }

        #region Creazione Assi
        /*
        /// <summary>
        /// Crea dinamicamente il bottone "Z1" all’interno del pannello di navigazione
        /// <see cref="pnl_navigation"/> e aggiunge un'etichetta centrata sotto di esso.
        /// </summary>
        /// <remarks>
        /// Questo metodo esegue le seguenti operazioni:
        /// <list type="number">
        ///     <item>Istanzia un <see cref="CustomButton"/> con dimensioni, posizione, colore di sfondo, immagine, bordi arrotondati e bordo colorato.</item>
        ///     <item>Iscrive l’evento <see cref="ClickEvent_goToZ1"/> al click del bottone.</item>
        ///     <item>Aggiunge il bottone al <see cref="Panel"/> di navigazione <see cref="pnl_navigation"/> e lo porta in primo piano.</item>
        ///     <item>Istanzia una <see cref="Label"/> con il testo "CABINA", centrata orizzontalmente sotto il bottone.</item>
        ///     <item>Calcola dinamicamente la posizione della label in base alla larghezza del bottone e della label.</item>
        ///     <item>Aggiunge la label al <see cref="Panel"/> di navigazione e la porta in primo piano.</item>
        /// </list>
        /// </remarks>
        /// <example>
        /// Esempio di utilizzo:
        /// <code>
        /// CreateCabinButton();
        /// </code>
        /// </example>
        private void CreateZ1Button()
        {
            // Creazione del CustomButton
            customBtn_z1 = new CustomButton
            {
                Name = "customBtn_z1",
                Size = new Size(xSize, ySize),
                Location = new Point(45, 55), // relativo a panelNavigation
                BackgroundColor = SystemColors.ActiveBorder,
                Text = "Z1",
                ForeColor = Color.Black,
                Font = new Font("Arial", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter, // centra il testo
                BackgroundImageLayout = ImageLayout.Center,
                BorderRadius = 15,
                BorderSize = 2,
                BorderColor = Color.DimGray
            };

            // Evento click per navigare
            customBtn_z1.Click += ClickEvent_goToZ1;

            // Aggiunge il bottone al pannello
            pnl_axis.Controls.Add(customBtn_z1);
            customBtn_z1.BringToFront();

            // Creazione del CustomButton
            customBtn_OFFz1 = new CustomButton
            {
                Name = "customBtn_OFFz1",
                Size = new Size(xSize, ySize),
                Location = new Point(45 + xOffsetZButton, 55), // relativo a panelNavigation
                BackgroundColor = Color.RoyalBlue,
                Text = "OFF",
                ForeColor = Color.White,
                Font = new Font("Arial", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter, // centra il testo
                BackgroundImageLayout = ImageLayout.Center,
                BorderRadius = 15,
                BorderSize = 2,
                BorderColor = Color.DimGray
            };

            // Evento click per navigare
            //customBtn_z1.Click += ClickEvent_goToZ1;

            // Aggiunge il bottone al pannello
            pnl_axis.Controls.Add(customBtn_OFFz1);
            customBtn_OFFz1.BringToFront();
        }

        /// <summary>
        /// Crea dinamicamente il bottone "Z2" all’interno del pannello di navigazione
        /// <see cref="pnl_navigation"/> e aggiunge un'etichetta centrata sotto di esso.
        /// </summary>
        /// <remarks>
        /// Questo metodo esegue le seguenti operazioni:
        /// <list type="number">
        ///     <item>Istanzia un <see cref="CustomButton"/> con dimensioni, posizione, colore di sfondo, immagine, bordi arrotondati e bordo colorato.</item>
        ///     <item>Iscrive l’evento <see cref="ClickEvent_goToZ2"/> al click del bottone.</item>
        ///     <item>Aggiunge il bottone al <see cref="Panel"/> di navigazione <see cref="pnl_navigation"/> e lo porta in primo piano.</item>
        ///     <item>Istanzia una <see cref="Label"/> con il testo "CABINA", centrata orizzontalmente sotto il bottone.</item>
        ///     <item>Calcola dinamicamente la posizione della label in base alla larghezza del bottone e della label.</item>
        ///     <item>Aggiunge la label al <see cref="Panel"/> di navigazione e la porta in primo piano.</item>
        /// </list>
        /// </remarks>
        /// <example>
        /// Esempio di utilizzo:
        /// <code>
        /// CreateCabinButton();
        /// </code>
        /// </example>
        private void CreateZ2Button()
        {
            // Creazione del CustomButton
            customBtn_z2 = new CustomButton
            {
                Name = "customBtn_z2",
                Size = new Size(xSize, ySize),
                Location = new Point(45, 155), // relativo a panelNavigation
                BackgroundColor = SystemColors.ActiveBorder,
                Text = "Z2",
                ForeColor = Color.Black,
                Font = new Font("Arial", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter, // centra il testo
                BackgroundImageLayout = ImageLayout.Center,
                BorderRadius = 15,
                BorderSize = 2,
                BorderColor = Color.DimGray
            };

            // Evento click per navigare
            customBtn_z2.Click += ClickEvent_goToZ2;

            // Aggiunge il bottone al pannello
            pnl_axis.Controls.Add(customBtn_z2);
            customBtn_z2.BringToFront();

            // Creazione del CustomButton
            customBtn_OFFz2 = new CustomButton
            {
                Name = "customBtn_OFFz2",
                Size = new Size(xSize, ySize),
                Location = new Point(45 + xOffsetZButton, 155), // relativo a panelNavigation
                BackgroundColor = Color.RoyalBlue,
                Text = "OFF",
                ForeColor = Color.White,
                Font = new Font("Arial", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter, // centra il testo
                BackgroundImageLayout = ImageLayout.Center,
                BorderRadius = 15,
                BorderSize = 2,
                BorderColor = Color.DimGray
            };

            // Evento click per navigare
            //customBtn_z1.Click += ClickEvent_goToZ1;

            // Aggiunge il bottone al pannello
            pnl_axis.Controls.Add(customBtn_OFFz2);
            customBtn_OFFz2.BringToFront();
        }

        /// <summary>
        /// Crea dinamicamente il bottone "Z3" all’interno del pannello di navigazione
        /// <see cref="pnl_navigation"/> e aggiunge un'etichetta centrata sotto di esso.
        /// </summary>
        /// <remarks>
        /// Questo metodo esegue le seguenti operazioni:
        /// <list type="number">
        ///     <item>Istanzia un <see cref="CustomButton"/> con dimensioni, posizione, colore di sfondo, immagine, bordi arrotondati e bordo colorato.</item>
        ///     <item>Iscrive l’evento <see cref="ClickEvent_goToZ3"/> al click del bottone.</item>
        ///     <item>Aggiunge il bottone al <see cref="Panel"/> di navigazione <see cref="pnl_navigation"/> e lo porta in primo piano.</item>
        ///     <item>Istanzia una <see cref="Label"/> con il testo "CABINA", centrata orizzontalmente sotto il bottone.</item>
        ///     <item>Calcola dinamicamente la posizione della label in base alla larghezza del bottone e della label.</item>
        ///     <item>Aggiunge la label al <see cref="Panel"/> di navigazione e la porta in primo piano.</item>
        /// </list>
        /// </remarks>
        /// <example>
        /// Esempio di utilizzo:
        /// <code>
        /// CreateCabinButton();
        /// </code>
        /// </example>
        private void CreateZ3Button()
        {
            // Creazione del CustomButton
            customBtn_z3 = new CustomButton
            {
                Name = "customBtn_z3",
                Size = new Size(xSize, ySize),
                Location = new Point(45, 255), // relativo a panelNavigation
                BackgroundColor = SystemColors.ActiveBorder,
                Text = "Z3",
                ForeColor = Color.Black,
                Font = new Font("Arial", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter, // centra il testo
                BackgroundImageLayout = ImageLayout.Center,
                BorderRadius = 15,
                BorderSize = 2,
                BorderColor = Color.DimGray
            };

            // Evento click per navigare
            customBtn_z3.Click += ClickEvent_goToZ3;

            // Aggiunge il bottone al pannello
            pnl_axis.Controls.Add(customBtn_z3);
            customBtn_z3.BringToFront();

            // Creazione del CustomButton
            customBtn_OFFz3 = new CustomButton
            {
                Name = "customBtn_OFFz3",
                Size = new Size(xSize, ySize),
                Location = new Point(45 + xOffsetZButton, 255), // relativo a panelNavigation
                BackgroundColor = Color.RoyalBlue,
                Text = "OFF",
                ForeColor = Color.White,
                Font = new Font("Arial", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter, // centra il testo
                BackgroundImageLayout = ImageLayout.Center,
                BorderRadius = 15,
                BorderSize = 2,
                BorderColor = Color.DimGray
            };

            // Evento click per navigare
            //customBtn_z1.Click += ClickEvent_goToZ1;

            // Aggiunge il bottone al pannello
            pnl_axis.Controls.Add(customBtn_OFFz3);
            customBtn_OFFz3.BringToFront();
        }

        /// <summary>
        /// Crea dinamicamente il bottone "Z4" all’interno del pannello di navigazione
        /// <see cref="pnl_navigation"/> e aggiunge un'etichetta centrata sotto di esso.
        /// </summary>
        /// <remarks>
        /// Questo metodo esegue le seguenti operazioni:
        /// <list type="number">
        ///     <item>Istanzia un <see cref="CustomButton"/> con dimensioni, posizione, colore di sfondo, immagine, bordi arrotondati e bordo colorato.</item>
        ///     <item>Iscrive l’evento <see cref="ClickEvent_goToZ4"/> al click del bottone.</item>
        ///     <item>Aggiunge il bottone al <see cref="Panel"/> di navigazione <see cref="pnl_navigation"/> e lo porta in primo piano.</item>
        ///     <item>Istanzia una <see cref="Label"/> con il testo "CABINA", centrata orizzontalmente sotto il bottone.</item>
        ///     <item>Calcola dinamicamente la posizione della label in base alla larghezza del bottone e della label.</item>
        ///     <item>Aggiunge la label al <see cref="Panel"/> di navigazione e la porta in primo piano.</item>
        /// </list>
        /// </remarks>
        /// <example>
        /// Esempio di utilizzo:
        /// <code>
        /// CreateCabinButton();
        /// </code>
        /// </example>
        private void CreateZ4Button()
        {
            // Creazione del CustomButton
            customBtn_z4 = new CustomButton
            {
                Name = "customBtn_z4",
                Size = new Size(xSize, ySize),
                Location = new Point(45, 355), // relativo a panelNavigation
                BackgroundColor = SystemColors.ActiveBorder,
                Text = "Z2",
                ForeColor = Color.Black,
                Font = new Font("Arial", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter, // centra il testo
                BackgroundImageLayout = ImageLayout.Center,
                BorderRadius = 15,
                BorderSize = 2,
                BorderColor = Color.DimGray
            };

            // Evento click per navigare
            customBtn_z4.Click += ClickEvent_goToZ4;

            // Aggiunge il bottone al pannello
            pnl_axis.Controls.Add(customBtn_z4);
            customBtn_z4.BringToFront();

            // Creazione del CustomButton
            customBtn_OFFz4 = new CustomButton
            {
                Name = "customBtn_OFFz4",
                Size = new Size(xSize, ySize),
                Location = new Point(45 + xOffsetZButton, 355), // relativo a panelNavigation
                BackgroundColor = Color.RoyalBlue,
                Text = "OFF",
                ForeColor = Color.White,
                Font = new Font("Arial", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter, // centra il testo
                BackgroundImageLayout = ImageLayout.Center,
                BorderRadius = 15,
                BorderSize = 2,
                BorderColor = Color.DimGray
            };

            // Evento click per navigare
            //customBtn_z1.Click += ClickEvent_goToZ1;

            // Aggiunge il bottone al pannello
            pnl_axis.Controls.Add(customBtn_OFFz4);
            customBtn_OFFz4.BringToFront();
        }

        /// <summary>
        /// Crea dinamicamente il bottone "Z5" all’interno del pannello di navigazione
        /// <see cref="pnl_navigation"/> e aggiunge un'etichetta centrata sotto di esso.
        /// </summary>
        /// <remarks>
        /// Questo metodo esegue le seguenti operazioni:
        /// <list type="number">
        ///     <item>Istanzia un <see cref="CustomButton"/> con dimensioni, posizione, colore di sfondo, immagine, bordi arrotondati e bordo colorato.</item>
        ///     <item>Iscrive l’evento <see cref="ClickEvent_goToZ5"/> al click del bottone.</item>
        ///     <item>Aggiunge il bottone al <see cref="Panel"/> di navigazione <see cref="pnl_navigation"/> e lo porta in primo piano.</item>
        ///     <item>Istanzia una <see cref="Label"/> con il testo "CABINA", centrata orizzontalmente sotto il bottone.</item>
        ///     <item>Calcola dinamicamente la posizione della label in base alla larghezza del bottone e della label.</item>
        ///     <item>Aggiunge la label al <see cref="Panel"/> di navigazione e la porta in primo piano.</item>
        /// </list>
        /// </remarks>
        /// <example>
        /// Esempio di utilizzo:
        /// <code>
        /// CreateCabinButton();
        /// </code>
        /// </example>
        private void CreateZ5Button()
        {
            // Creazione del CustomButton
            customBtn_z5 = new CustomButton
            {
                Name = "customBtn_z5",
                Size = new Size(xSize, ySize),
                Location = new Point(210, 55), // relativo a panelNavigation
                BackgroundColor = SystemColors.ActiveBorder,
                Text = "Z5",
                ForeColor = Color.Black,
                Font = new Font("Arial", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter, // centra il testo
                BackgroundImageLayout = ImageLayout.Center,
                BorderRadius = 15,
                BorderSize = 2,
                BorderColor = Color.DimGray
            };

            // Evento click per navigare
            customBtn_z5.Click += ClickEvent_goToZ5;

            // Aggiunge il bottone al pannello
            pnl_axis.Controls.Add(customBtn_z5);
            customBtn_z5.BringToFront();

            // Creazione del CustomButton
            customBtn_OFFz5 = new CustomButton
            {
                Name = "customBtn_OFFz5",
                Size = new Size(xSize, ySize),
                Location = new Point(210 + xOffsetZButton, 55), // relativo a panelNavigation
                BackgroundColor = Color.RoyalBlue,
                Text = "OFF",
                ForeColor = Color.White,
                Font = new Font("Arial", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter, // centra il testo
                BackgroundImageLayout = ImageLayout.Center,
                BorderRadius = 15,
                BorderSize = 2,
                BorderColor = Color.DimGray
            };

            // Evento click per navigare
            //customBtn_z1.Click += ClickEvent_goToZ1;

            // Aggiunge il bottone al pannello
            pnl_axis.Controls.Add(customBtn_OFFz5);
            customBtn_OFFz5.BringToFront();
        }

        /// <summary>
        /// Crea dinamicamente il bottone "Z6" all’interno del pannello di navigazione
        /// <see cref="pnl_navigation"/> e aggiunge un'etichetta centrata sotto di esso.
        /// </summary>
        /// <remarks>
        /// Questo metodo esegue le seguenti operazioni:
        /// <list type="number">
        ///     <item>Istanzia un <see cref="CustomButton"/> con dimensioni, posizione, colore di sfondo, immagine, bordi arrotondati e bordo colorato.</item>
        ///     <item>Iscrive l’evento <see cref="ClickEvent_goToZ6"/> al click del bottone.</item>
        ///     <item>Aggiunge il bottone al <see cref="Panel"/> di navigazione <see cref="pnl_navigation"/> e lo porta in primo piano.</item>
        ///     <item>Istanzia una <see cref="Label"/> con il testo "CABINA", centrata orizzontalmente sotto il bottone.</item>
        ///     <item>Calcola dinamicamente la posizione della label in base alla larghezza del bottone e della label.</item>
        ///     <item>Aggiunge la label al <see cref="Panel"/> di navigazione e la porta in primo piano.</item>
        /// </list>
        /// </remarks>
        /// <example>
        /// Esempio di utilizzo:
        /// <code>
        /// CreateCabinButton();
        /// </code>
        /// </example>
        private void CreateZ6Button()
        {
            // Creazione del CustomButton
            customBtn_z6 = new CustomButton
            {
                Name = "customBtn_z6",
                Size = new Size(xSize, ySize),
                Location = new Point(210, 155), // relativo a panelNavigation
                BackgroundColor = SystemColors.ActiveBorder,
                Text = "Z6",
                ForeColor = Color.Black,
                Font = new Font("Arial", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter, // centra il testo
                BackgroundImageLayout = ImageLayout.Center,
                BorderRadius = 15,
                BorderSize = 2,
                BorderColor = Color.DimGray
            };

            // Evento click per navigare
            customBtn_z6.Click += ClickEvent_goToZ6;

            // Aggiunge il bottone al pannello
            pnl_axis.Controls.Add(customBtn_z6);
            customBtn_z6.BringToFront();

            // Creazione del CustomButton
            customBtn_OFFz6 = new CustomButton
            {
                Name = "customBtn_OFFz6",
                Size = new Size(xSize, ySize),
                Location = new Point(210 + xOffsetZButton, 155), // relativo a panelNavigation
                BackgroundColor = Color.RoyalBlue,
                Text = "OFF",
                ForeColor = Color.White,
                Font = new Font("Arial", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter, // centra il testo
                BackgroundImageLayout = ImageLayout.Center,
                BorderRadius = 15,
                BorderSize = 2,
                BorderColor = Color.DimGray
            };

            // Evento click per navigare
            //customBtn_z1.Click += ClickEvent_goToZ1;

            // Aggiunge il bottone al pannello
            pnl_axis.Controls.Add(customBtn_OFFz6);
            customBtn_OFFz6.BringToFront();
        }

        /// <summary>
        /// Crea dinamicamente il bottone "Z7" all’interno del pannello di navigazione
        /// <see cref="pnl_navigation"/> e aggiunge un'etichetta centrata sotto di esso.
        /// </summary>
        /// <remarks>
        /// Questo metodo esegue le seguenti operazioni:
        /// <list type="number">
        ///     <item>Istanzia un <see cref="CustomButton"/> con dimensioni, posizione, colore di sfondo, immagine, bordi arrotondati e bordo colorato.</item>
        ///     <item>Iscrive l’evento <see cref="ClickEvent_goToZ7"/> al click del bottone.</item>
        ///     <item>Aggiunge il bottone al <see cref="Panel"/> di navigazione <see cref="pnl_navigation"/> e lo porta in primo piano.</item>
        ///     <item>Istanzia una <see cref="Label"/> con il testo "CABINA", centrata orizzontalmente sotto il bottone.</item>
        ///     <item>Calcola dinamicamente la posizione della label in base alla larghezza del bottone e della label.</item>
        ///     <item>Aggiunge la label al <see cref="Panel"/> di navigazione e la porta in primo piano.</item>
        /// </list>
        /// </remarks>
        /// <example>
        /// Esempio di utilizzo:
        /// <code>
        /// CreateCabinButton();
        /// </code>
        /// </example>
        private void CreateZ7Button()
        {
            // Creazione del CustomButton
            customBtn_z7 = new CustomButton
            {
                Name = "customBtn_z7",
                Size = new Size(xSize, ySize),
                Location = new Point(210, 255), // relativo a panelNavigation
                BackgroundColor = SystemColors.ActiveBorder,
                Text = "Z7",
                ForeColor = Color.Black,
                Font = new Font("Arial", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter, // centra il testo
                BackgroundImageLayout = ImageLayout.Center,
                BorderRadius = 15,
                BorderSize = 2,
                BorderColor = Color.DimGray
            };

            // Evento click per navigare
            customBtn_z7.Click += ClickEvent_goToZ7;

            // Aggiunge il bottone al pannello
            pnl_axis.Controls.Add(customBtn_z7);
            customBtn_z7.BringToFront();

            // Creazione del CustomButton
            customBtn_OFFz7 = new CustomButton
            {
                Name = "customBtn_OFFz7",
                Size = new Size(xSize, ySize),
                Location = new Point(210 + xOffsetZButton, 255), // relativo a panelNavigation
                BackgroundColor = Color.RoyalBlue,
                Text = "OFF",
                ForeColor = Color.White,
                Font = new Font("Arial", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter, // centra il testo
                BackgroundImageLayout = ImageLayout.Center,
                BorderRadius = 15,
                BorderSize = 2,
                BorderColor = Color.DimGray
            };

            // Evento click per navigare
            //customBtn_z1.Click += ClickEvent_goToZ1;

            // Aggiunge il bottone al pannello
            pnl_axis.Controls.Add(customBtn_OFFz7);
            customBtn_OFFz7.BringToFront();
        }

        /// <summary>
        /// Crea dinamicamente il bottone "Z7" all’interno del pannello di navigazione
        /// <see cref="pnl_navigation"/> e aggiunge un'etichetta centrata sotto di esso.
        /// </summary>
        /// <remarks>
        /// Questo metodo esegue le seguenti operazioni:
        /// <list type="number">
        ///     <item>Istanzia un <see cref="CustomButton"/> con dimensioni, posizione, colore di sfondo, immagine, bordi arrotondati e bordo colorato.</item>
        ///     <item>Iscrive l’evento <see cref="ClickEvent_goToZ8"/> al click del bottone.</item>
        ///     <item>Aggiunge il bottone al <see cref="Panel"/> di navigazione <see cref="pnl_navigation"/> e lo porta in primo piano.</item>
        ///     <item>Istanzia una <see cref="Label"/> con il testo "CABINA", centrata orizzontalmente sotto il bottone.</item>
        ///     <item>Calcola dinamicamente la posizione della label in base alla larghezza del bottone e della label.</item>
        ///     <item>Aggiunge la label al <see cref="Panel"/> di navigazione e la porta in primo piano.</item>
        /// </list>
        /// </remarks>
        /// <example>
        /// Esempio di utilizzo:
        /// <code>
        /// CreateCabinButton();
        /// </code>
        /// </example>
        private void CreateZ8Button()
        {
            // Creazione del CustomButton
            customBtn_z8 = new CustomButton
            {
                Name = "customBtn_z8",
                Size = new Size(xSize, ySize),
                Location = new Point(210, 355), // relativo a panelNavigation
                BackgroundColor = SystemColors.ActiveBorder,
                Text = "Z8",
                ForeColor = Color.Black,
                Font = new Font("Arial", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter, // centra il testo
                BackgroundImageLayout = ImageLayout.Center,
                BorderRadius = 15,
                BorderSize = 2,
                BorderColor = Color.DimGray
            };

            // Evento click per navigare
            customBtn_z8.Click += ClickEvent_goToZ8;

            // Aggiunge il bottone al pannello
            pnl_axis.Controls.Add(customBtn_z8);
            customBtn_z8.BringToFront();

            // Creazione del CustomButton
            customBtn_OFFz8 = new CustomButton
            {
                Name = "customBtn_OFFz8",
                Size = new Size(xSize, ySize),
                Location = new Point(210 + xOffsetZButton, 355), // relativo a panelNavigation
                BackgroundColor = Color.RoyalBlue,
                Text = "OFF",
                ForeColor = Color.White,
                Font = new Font("Arial", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter, // centra il testo
                BackgroundImageLayout = ImageLayout.Center,
                BorderRadius = 15,
                BorderSize = 2,
                BorderColor = Color.DimGray
            };

            // Evento click per navigare
            //customBtn_z1.Click += ClickEvent_goToZ1;

            // Aggiunge il bottone al pannello
            pnl_axis.Controls.Add(customBtn_OFFz8);
            customBtn_OFFz8.BringToFront();
        }
        */
        #endregion

        #region Creazione opzioni
        /*
        /// <summary>
        /// Crea dinamicamente il bottone "LASER" all’interno del pannello pnl_options
        /// </summary>
        private void CreateLaserButton()
        {
            // Creazione del CustomButton
            customBtn_laser = new CustomButton
            {
                Name = "customBtn_laser",
                Size = new Size(100, 55),
                Location = new Point(45, 55), // relativo a panelNavigation
                BackgroundColor = Color.Black,
                BackgroundImage = Resources.laser32White,
                BackgroundImageLayout = ImageLayout.Center,
                BorderRadius = 15,
                BorderSize = 1,
                BorderColor = Color.DimGray
            };

            // Aggiunge il bottone al pannello
            pnl_options.Controls.Add(customBtn_laser);
            customBtn_laser.Left = (pnl_options.ClientSize.Width - customBtn_laser.Width) / 2;
            customBtn_laser.BringToFront();

            // Creazione della label centrata sotto il bottone
            lb_laser = new Label
            {
                Text = "LASER",
                AutoSize = true, // importante per centrare correttamente
                ForeColor = Color.White,
                Font = new Font("Arial", 8, FontStyle.Bold),
                TextAlign = ContentAlignment.TopCenter // centra il testo
            };

            // Calcolo della posizione della label
            int labelX = customBtn_laser.Left + (customBtn_laser.Width - lb_laser.PreferredWidth) / 2;
            int labelY = customBtn_laser.Bottom + 5; // 5px di margine sotto il bottone
            lb_laser.Location = new Point(labelX, labelY);

            // Aggiunge la label al pannello
            pnl_options.Controls.Add(lb_laser);
            lb_laser.BringToFront();
        }

        /// <summary>
        /// Crea dinamicamente il bottone "3D" all’interno del pannello pnl_options
        /// </summary>
        private void Create3DButton()
        {
            // Creazione del CustomButton
            customBtn_3D = new CustomButton
            {
                Name = "customBtn_3D",
                Size = new Size(100, 55),
                Location = new Point(45, 155), // relativo a panelNavigation
                BackgroundColor = Color.Black,
                BackgroundImage = Resources._3d32White,
                BackgroundImageLayout = ImageLayout.Center,
                BorderRadius = 15,
                BorderSize = 1,
                BorderColor = Color.DimGray
            };

            // Aggiunge il bottone al pannello
            pnl_options.Controls.Add(customBtn_3D);
            customBtn_3D.Left = (pnl_options.ClientSize.Width - customBtn_3D.Width) / 2;
            customBtn_3D.BringToFront();

            // Creazione della label centrata sotto il bottone
            lb_3D = new Label
            {
                Text = "LIVE 3D",
                AutoSize = true, // importante per centrare correttamente
                ForeColor = Color.White,
                Font = new Font("Arial", 8, FontStyle.Bold),
                TextAlign = ContentAlignment.TopCenter // centra il testo
            };

            // Calcolo della posizione della label
            int labelX = customBtn_3D.Left + (customBtn_3D.Width - lb_3D.PreferredWidth) / 2;
            int labelY = customBtn_3D.Bottom + 5; // 5px di margine sotto il bottone
            lb_3D.Location = new Point(labelX, labelY);

            // Aggiunge la label al pannello
            pnl_options.Controls.Add(lb_3D);
            lb_3D.BringToFront();
        }

        /// <summary>
        /// Crea dinamicamente il bottone "LUCI CABINA" all’interno del pannello pnl_options
        /// </summary>
        private void CreateLightsButton()
        {
            // Creazione del CustomButton
            customBtn_lights = new CustomButton
            {
                Name = "customBtn_lights",
                Size = new Size(100, 55),
                Location = new Point(45, 255), // relativo a panelNavigation
                BackgroundColor = Color.Black,
                BackgroundImage = Resources.light32White,
                BackgroundImageLayout = ImageLayout.Center,
                BorderRadius = 15,
                BorderSize = 1,
                BorderColor = Color.DimGray
            };

            // Aggiunge il bottone al pannello
            pnl_options.Controls.Add(customBtn_lights);
            customBtn_lights.Left = (pnl_options.ClientSize.Width - customBtn_lights.Width) / 2;
            customBtn_lights.BringToFront();

            // Creazione della label centrata sotto il bottone
            lb_lights = new Label
            {
                Text = "LUCI CABINA",
                AutoSize = true, // importante per centrare correttamente
                ForeColor = Color.White,
                Font = new Font("Arial", 8, FontStyle.Bold),
                TextAlign = ContentAlignment.TopCenter // centra il testo
            };

            // Calcolo della posizione della label
            int labelX = customBtn_lights.Left + (customBtn_lights.Width - lb_lights.PreferredWidth) / 2;
            int labelY = customBtn_lights.Bottom + 5; // 5px di margine sotto il bottone
            lb_lights.Location = new Point(labelX, labelY);

            // Aggiunge la label al pannello
            pnl_options.Controls.Add(lb_lights);
            lb_lights.BringToFront();
        }
        */
        #endregion

        #region Creazione hub
        /*
        /// <summary>
        /// Crea dinamicamente il bottone "UP" all’interno di pnl_hub
        ///
        private void CreateUpButton()
        {
            // Creazione del CustomButton
            customBtn_up = new CustomButton
            {
                Name = "customBtn_up",
                Size = new Size(xSize, ySize),
                Location = new Point(50, 55), // relativo a panelNavigation
                BackgroundColor = SystemColors.ActiveBorder,
                ForeColor = Color.Black,
                Font = new Font("Arial", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter, // centra il testo
                BackgroundImage = Resources.up,
                BackgroundImageLayout = ImageLayout.Center,
                BorderRadius = 15,
                BorderSize = 2,
                BorderColor = Color.DimGray
            };

            // Evento click per navigare
            //customBtn_z1.Click += ClickEvent_goToZ1;

            // Aggiunge il bottone al pannello
            pnl_hub.Controls.Add(customBtn_up);
            customBtn_up.BringToFront();

        }

        /// <summary>
        /// Crea dinamicamente il bottone "DOWN" all’interno di pnl_hub
        ///
        private void CreateDownButton()
        {
            // Creazione del CustomButton
            customBtn_down = new CustomButton
            {
                Name = "customBtn_down",
                Size = new Size(xSize, ySize),
                Location = new Point(50, 155), // relativo a panelNavigation
                BackgroundColor = SystemColors.ActiveBorder,
                ForeColor = Color.Black,
                Font = new Font("Arial", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter, // centra il testo
                BackgroundImage = Resources.down,
                BackgroundImageLayout = ImageLayout.Center,
                BorderRadius = 15,
                BorderSize = 2,
                BorderColor = Color.DimGray
            };

            // Evento click per navigare
            //customBtn_z1.Click += ClickEvent_goToZ1;

            // Aggiunge il bottone al pannello
            pnl_hub.Controls.Add(customBtn_down);
            customBtn_down.BringToFront();

        }

        /// <summary>
        /// Crea dinamicamente il panel con all'interno la label valHubValue
        ///
        private void CreateValHubPanelLabel()
        {
            // Panel custom
            customPnl_valHub = new CustomPanel
            {
                Name = "customPnl_valHub",
                Size = new Size(55, 55),
                Location = new Point(150, 55), // relativo a pnl_hub
                BackColor = Color.DarkRed,
                ForeColor = Color.White,
                Font = new Font("Arial", 12, FontStyle.Bold),
                BorderRadius = 15,
                BorderColor = Color.DimGray,
                BorderSize = 2
            };

            pnl_hub.Controls.Add(customPnl_valHub);
            customPnl_valHub.BringToFront();

            // Label interna centrata
            var lblValue = new Label
            {
                Name = "lblValHub",
                Text = "5", // valore esempio
                AutoSize = true,
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Centratura nel panel
            lblValue.Location = new Point(
                (customPnl_valHub.Width - lblValue.PreferredWidth) / 2,
                (customPnl_valHub.Height - lblValue.PreferredHeight) / 2
            );

            customPnl_valHub.Controls.Add(lblValue);
            lblValue.BringToFront();
        }

        /// <summary>
        /// Crea dinamicamente il bottone "OFF" all’interno di pnl_hub
        /// </summary>
        private void CreateHubOFFButton()
        {
            // Creazione del CustomButton
            customBtn_hubOFF = new CustomButton
            {
                Name = "customBtn_hubOFF",
                Size = new Size(xSize, ySize),
                Location = new Point(50 + 100, 155), // relativo a panelNavigation
                BackgroundColor = Color.RoyalBlue,
                Text = "OFF",
                ForeColor = Color.White,
                Font = new Font("Arial", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter, // centra il testo
                BackgroundImageLayout = ImageLayout.Center,
                BorderRadius = 15,
                BorderSize = 2,
                BorderColor = Color.DimGray
            };

            // Evento click per navigare
            //customBtn_z1.Click += ClickEvent_goToZ1;

            // Aggiunge il bottone al pannello
            pnl_hub.Controls.Add(customBtn_hubOFF);
            customBtn_hubOFF.BringToFront();
        }
        */
        #endregion

        #endregion

        #region Eventi di UC_HomePage

        /// <summary>
        /// Apre la gestione dell'asse 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_goToZ1(object sender, EventArgs e)
        {
            FormHomePage._navigator.Navigate("Axis", 1);
        }

        /// <summary>
        /// Apre la gestione dell'asse 2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_goToZ2(object sender, EventArgs e)
        {
            FormHomePage._navigator.Navigate("Axis", 2);
        }

        /// <summary>
        /// Apre la gestione dell'asse 3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_goToZ3(object sender, EventArgs e)
        {
            FormHomePage._navigator.Navigate("Axis", 3);
        }

        /// <summary>
        /// Apre la gestione dell'asse 4
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_goToZ4(object sender, EventArgs e)
        {
            FormHomePage._navigator.Navigate("Axis", 4);
        }

        /// <summary>
        /// Apre la gestione dell'asse 5
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_goToZ5(object sender, EventArgs e)
        {
            FormHomePage._navigator.Navigate("Axis", 5);
        }

        /// <summary>
        /// Apre la gestione dell'asse 6
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_goToZ6(object sender, EventArgs e)
        {
            FormHomePage._navigator.Navigate("Axis", 6);
        }

        /// <summary>
        /// Apre la gestione dell'asse 7
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_goToZ7(object sender, EventArgs e)
        {
            FormHomePage._navigator.Navigate("Axis", 7);
        }

        /// <summary>
        /// Apre la gestione dell'asse 8
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_goToZ8(object sender, EventArgs e)
        {
            FormHomePage._navigator.Navigate("Axis", 8);
        }

        /// <summary>
        /// Caricamento della homePage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UC_HomePage_Load(object sender, EventArgs e)
        {
            //InitDynamicControlsAxis();
            //InitDynamicControlsOptions();
            //InitDynamicControlsHub();
        }

        #endregion


    }
}
