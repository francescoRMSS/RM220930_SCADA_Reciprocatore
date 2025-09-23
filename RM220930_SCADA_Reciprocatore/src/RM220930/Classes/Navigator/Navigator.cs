using System.Collections.Generic;
using System.Windows.Forms;

using System;
using RM.src.RM220930.Classes.Navigator;

/// <summary>
/// Gestore centralizzato della navigazione tra UserControl in una WinForm.
/// 
/// La classe permette di registrare UserControl associandoli a una chiave,
/// e di navigare tra essi caricandoli dinamicamente all’interno di un <see cref="Panel"/> host.
/// 
/// Include supporto per:
/// - Lazy Loading (le pagine vengono istanziate solo al primo utilizzo)
/// - Caching (le pagine già create vengono riutilizzate)
/// - Passaggio di parametri tramite l’interfaccia <see cref="INavigable"/>
/// </summary>
public class Navigator
{
    private readonly Panel _hostPanel;
    private readonly Dictionary<string, Type> _pages;
    private readonly Dictionary<string, UserControl> _cache;

    /// <summary>
    /// Inizializza una nuova istanza del <see cref="Navigator"/>.
    /// </summary>
    /// <param name="hostPanel">
    /// Il <see cref="Panel"/> contenitore che ospiterà le varie UserControl.
    /// Tipicamente si tratta di un panel nella Form principale.
    /// </param>
    public Navigator(Panel hostPanel)
    {
        _hostPanel = hostPanel ?? throw new ArgumentNullException(nameof(hostPanel));
        _pages = new Dictionary<string, Type>();
        _cache = new Dictionary<string, UserControl>();
    }

    /// <summary>
    /// Registra una nuova pagina navigabile.
    /// </summary>
    /// <param name="key">Chiave univoca che rappresenta la pagina (es. "Dashboard", "Settings").</param>
    /// <param name="pageType">Il tipo della UserControl da registrare.</param>
    /// <exception cref="ArgumentException">
    /// Sollevata se il tipo passato non eredita da <see cref="UserControl"/>.
    /// </exception>
    public void RegisterPage(string key, Type pageType)
    {
        if (!typeof(UserControl).IsAssignableFrom(pageType))
            throw new ArgumentException("Il tipo deve essere una UserControl valida.", nameof(pageType));

        _pages[key] = pageType;
    }

    /// <summary>
    /// Naviga verso una pagina registrata e la visualizza nel <see cref="_hostPanel"/>.
    /// Se la pagina è già stata creata in precedenza, viene riutilizzata dalla cache.
    /// </summary>
    /// <param name="key">Chiave della pagina da visualizzare.</param>
    /// <param name="parameter">
    /// Parametro opzionale da passare alla pagina.
    /// Se la UserControl implementa <see cref="INavigable"/>, riceverà questo parametro
    /// tramite il metodo <see cref="INavigable.OnNavigatedTo(object)"/>.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Sollevata se la chiave non corrisponde ad alcuna pagina registrata.
    /// </exception>
    public void Navigate(string key, object parameter = null)
    {
        if (!_pages.ContainsKey(key))
            throw new ArgumentException($"Pagina '{key}' non registrata.", nameof(key));

        UserControl page;

        // Se la pagina è già stata creata → riuso
        if (_cache.ContainsKey(key))
        {
            page = _cache[key];
        }
        else
        {
            page = (UserControl)Activator.CreateInstance(_pages[key]);
            page.Dock = DockStyle.Fill;
            _cache[key] = page;

            if (page is INavigationRequester requester)
            {
                requester.NavigateRequested += (s, e) => Navigate(e.PageKey, e.Parameter);
            }
        }

        // 👇 Nuovo controllo: se la pagina è già mostrata, non fare nulla
        if (_hostPanel.Controls.Count > 0 && _hostPanel.Controls[0] == page)
        {
            return;
        }

        // Rimpiazzo contenuto del pannello host
        _hostPanel.Controls.Clear();
        _hostPanel.Controls.Add(page);

        // Passaggio parametri, se implementa INavigable
        if (page is INavigable navigablePage)
        {
            navigablePage.OnNavigatedTo(parameter);
        }
    }

}
