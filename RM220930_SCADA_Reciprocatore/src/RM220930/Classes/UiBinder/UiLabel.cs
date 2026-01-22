using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RM.src.RM220930.Classes.UiBinder
{ 
    /// <summary>
    /// Contiene il riferimento ad una label
    /// Permette ad un task di usare la label senza eventi
    /// </summary>
    public class UiLabel
    {
        #region Proprietà di UiLabel

        /// <summary>
        /// Riferimento alla label
        /// </summary>
        public Label _label;

        #endregion

        #region Costruttori di UiLabel

        /// <summary>
        /// Costruisce il container della label
        /// </summary>
        /// <param name="label"></param>
        public UiLabel(Label label)
        {
            _label = label;
        }

        /// <summary>
        /// Costruttore vuoto
        /// </summary>
        public UiLabel()
        {
            _label = null;
        }

        #endregion

        #region Metodi di UiLabel

        /// <summary>
        /// Cambia il riferimento alla label
        /// </summary>
        /// <param name="newLabel"></param>
        public void ChangeLabelRef(Label newLabel)
        {
            _label = newLabel;
        }

        /// <summary>
        /// Scrive alla label se possibile
        /// </summary>
        /// <param name="text"></param>
        public void Write(object text)
        {
            if (_label == null || _label.IsDisposed) return;

            string textToWrite = text?.ToString() ?? "";

            if (_label.InvokeRequired)
            {
                // Eseguo l'operazione sul thread della UI in modo asincrono (non blocca il robot)
                _label.BeginInvoke(new MethodInvoker(() =>
                {
                    if (!_label.IsDisposed && _label.Text != textToWrite)
                    {
                        _label.Text = textToWrite;
                    }
                }));
            }
            else
            {
                // Siamo già sul thread giusto
                _label.Text = textToWrite;
            }
        }

        /// <summary>
        /// Restituisce la stringa nella label
        /// </summary>
        /// <returns></returns>
        public string ReadString()
        {
            if (_label == null || _label.IsDisposed) return "";

            if (_label.InvokeRequired)
            {
                // Invoke è sincrono (aspetta la risposta della UI)
                return (string)_label.Invoke(new Func<string>(() => _label.Text));
            }
            else
            {
                return _label.Text;
            }
        }

        /// <summary>
        /// Restituisce la stringa nella label come intero 
        /// </summary>
        /// <returns></returns>
        public int ReadInt()
        {
            string val = ReadString(); // Chiama il metodo thread-safe sopra
            if (string.IsNullOrWhiteSpace(val)) return 0;
            if (int.TryParse(val, NumberStyles.Any, CultureInfo.InvariantCulture, out int result)) return result;
            return 0;
        }

        /// <summary>
        /// Restituisce la stringa nella label come float
        /// </summary>
        /// <returns></returns>
        public float ReadFloat()
        {
            string val = ReadString();
            if (string.IsNullOrWhiteSpace(val)) return 0.0f;
            // Correzione virgola/punto inclusa
            val = val.Replace(",", ".");
            if (float.TryParse(val, NumberStyles.Any, CultureInfo.InvariantCulture, out float result)) return result;
            return 0.0f;
        }

        /// <summary>
        /// Restituisce la stringa nella label come double
        /// </summary>
        /// <returns></returns>
        public double ReadDouble()
        {
            string val = ReadString();
            if (string.IsNullOrWhiteSpace(val)) return 0.0;
            val = val.Replace(",", ".");
            if (double.TryParse(val, NumberStyles.Any, CultureInfo.InvariantCulture, out double result)) return result;
            return 0.0;
        }

        #endregion
    }
}
