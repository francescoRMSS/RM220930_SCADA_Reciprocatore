

/// <summary>
/// Interfaccia che definisce un contratto per le pagine navigabili.
/// Ogni UserControl che implementa questa interfaccia può ricevere parametri
/// al momento della navigazione.
/// </summary>
public interface INavigable
{
    /// <summary>
    /// Metodo invocato automaticamente quando la pagina viene visualizzata tramite il <see cref="Navigator"/>.
    /// </summary>
    /// <param name="parameter">
    /// Oggetto generico che rappresenta i dati passati alla pagina.
    /// Può essere null se non servono parametri specifici.
    /// 
    /// Esempi di utilizzo:
    /// - Passare una stringa con il nome dell’utente loggato.
    /// - Passare un DTO con impostazioni o dati caricati dal database.
    /// - Passare un identificativo (ID) per recuperare informazioni specifiche.
    /// </param>
    void OnNavigatedTo(object parameter);
}
