using RMLib.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RM.src.RM220930.Classes.UiBinder
{
    /// <summary>
    /// Rappresenta un pulsante a 2 stati
    /// </summary>
    public class BiStateButton
    {
        #region Proprietà di BiStateButton

        /// <summary>
        /// Riferimento a custom button
        /// </summary>
        public CustomButton _button;
        /// <summary>
        /// Contiene lo stato corrente del pulsante: 0 - non attivo, 1 - attivo
        /// </summary>
        private bool state;
        /// <summary>
        /// Colore pulsante non attivo - stato 0
        /// </summary>
        private readonly Color defaultNotActiveColor = Color.LightGray;
        /// <summary>
        /// Colore pulsante attivo - stato 1
        /// </summary>
        private readonly Color defaultActiveColor = Color.ForestGreen;
        /// <summary>
        /// Colore pulsante non attivo - stato 0
        /// </summary>
        private Color notActiveColor;
        /// <summary>
        /// Colore pulsante attivo - stato 1
        /// </summary>
        private Color activeColor;
        /// <summary>
        /// Immagine di sfondo per stato attivo
        /// </summary>
        private Image activeImage;
        /// <summary>
        /// Immagine di sfondo per stato non attivo
        /// </summary>
        private Image notActiveImage;
        /// <summary>
        /// Specifica se bisogna usare o no le immagini di sfondo
        /// </summary>
        private bool useImages;
        /// <summary>
        /// Specifica se bisogna ricolorare o no i puslanti
        /// </summary>
        private bool useColors;
        /// <summary>
        /// Testo utilizzato in stato active
        /// </summary>
        private string activeTextButton;
        /// <summary>
        /// Testo utilizzato in stato notActive
        /// </summary>
        private string notActiveTextButton;

        #endregion

        #region Costruttori

        /// <summary>
        /// Costruisce un riferimento a pulsante a due stati
        /// </summary>
        /// <param name="button"></param>
        public BiStateButton(CustomButton button)
        {
            _button = button;
            state = false;
            useImages = false;
            useColors = true;
            notActiveColor = defaultNotActiveColor;
            activeColor = defaultActiveColor;
            activeImage = null;
            notActiveImage = null;

            ChangeObjectColor(true); // Utilizzo colori di default
            ChangeObjectImage();
        }

        /// <summary>
        /// Costruisce un riferimento a pulsante a due stati
        /// </summary>
        /// <param name="button"></param>
        /// <param name="activeImg"></param>
        /// <param name="notActiveImg"></param>
        public BiStateButton(CustomButton button, Image activeImg, Image notActiveImg)
        {
            _button = button;
            state = false;
            useImages = true;
            useColors = false;
            notActiveColor = defaultNotActiveColor;
            activeColor = defaultActiveColor;
            activeImage = activeImg;
            notActiveImage = notActiveImg;

            ChangeObjectColor(true); // Utilizzo colori di default
            ChangeObjectImage();
        }

        /// <summary>
        /// Costruisce un riferimento a pulsante a due stati
        /// </summary>
        /// <param name="button"></param>
        /// <param name="activeCol"></param>
        /// <param name="notActiveCol"></param>
        public BiStateButton(CustomButton button, Color activeCol, Color notActiveCol)
        {
            _button = button;
            state = false;
            useImages = false;
            useColors = true;
            notActiveColor = activeCol;
            activeColor = notActiveCol;
            activeImage = null;
            notActiveImage = null;

            ChangeObjectColor(true); // Utilizzo colori di default
            ChangeObjectImage();
        }

        /// <summary>
        /// Costruisce un riferimento a pulsante a due stati
        /// </summary>
        /// <param name="button"></param>
        /// <param name="activeCol"></param>
        /// <param name="activeText"></param>
        /// <param name="notActiveCol"></param>
        /// <param name="notActiveText"></param>
        public BiStateButton(CustomButton button, Color activeCol, string activeText, Color notActiveCol, string notActiveText)
        {
            _button = button;
            useColors = true;
            activeColor = activeCol;
            notActiveColor = notActiveCol;
            activeImage = null;
            notActiveImage = null;
            activeTextButton = activeText;
            notActiveTextButton = notActiveText;

            ChangeObjectColor(false); // Utilizzo colori custom
            ChangeObjectImage();
        }

        /// <summary>
        /// Costruisce un riferimento a pulsante a due stati
        /// </summary>
        /// <param name="button"></param>
        /// <param name="activeCol"></param>
        /// <param name="notActiveCol"></param>
        /// <param name="activeImg"></param>
        /// <param name="notActiveImg"></param>
        public BiStateButton(CustomButton button, Color activeCol, Color notActiveCol, Image activeImg, Image notActiveImg)
        {
            _button = button;
            state = false;
            useImages = true;
            useColors = true;
            notActiveColor = notActiveCol;
            activeColor = activeCol;
            activeImage = activeImg;
            notActiveImage = notActiveImg;

            ChangeObjectColor(true); // Utilizzo colori di default
            ChangeObjectImage();
        }

        /// <summary>
        /// Costruisce un riferimento nullo
        /// </summary>
        public BiStateButton()
        {
            _button = null;
            state = false;
            useImages = false;
            useColors = true;
            notActiveColor = defaultNotActiveColor;
            activeColor = defaultActiveColor;
            activeImage = null;
            notActiveImage = null;

            ChangeObjectColor(true); // Utilizzo colori di default
            ChangeObjectImage();
        }

        #endregion

        #region Metodi di BiStateButton

        /// <summary>
        /// Cambia il riferimento al pulsante 
        /// </summary>
        /// <param name="newButton"></param>
        public void ChangeButtonRef(CustomButton newButton)
        {
            _button = newButton;
            ChangeObjectColor(true);
            ChangeObjectImage();
        }

        /// <summary>
        /// Cambia stato al puslante
        /// </summary>
        public void ChangeStatus()
        {
            state = !state;

            ChangeObjectColor(true);
            ChangeObjectImage();
        }

        /// <summary>
        /// Cambia stato in modo personalizzato cambiando testo e colori
        /// </summary>
        public void ChangeStatusCustom()
        {
            state = !state;

            ChangeObjectText();
            ChangeObjectColor(false);
            ChangeObjectImage();
        }

        /// <summary>
        /// Imposta lo stato desiderato
        /// </summary>
        /// <param name="newState"></param>
        public void ChangeStatus(bool newState)
        {
            state = newState;
            ChangeObjectColor(true);
            ChangeObjectImage();
        }

        /// <summary>
        /// Restituisce il colore di default corretto per lo stato
        /// </summary>
        /// <returns></returns>
        private Color GetCorrectStatusDefaultColor()
        {
            if (state) return defaultActiveColor;
            else return defaultNotActiveColor;
        }

        /// <summary>
        /// Restituisce il colore custom corretto per lo stato
        /// </summary>
        /// <returns></returns>
        private Color GetCorrectStatusCustomColor()
        {
            if (state) return activeColor;
            else return notActiveColor;
        }

        /// <summary>
        /// Restituisce il colore corretto per lo stato
        /// </summary>
        /// <returns></returns>
        private string GetCorrectText()
        {
            if (state) return activeTextButton;
            else return notActiveTextButton;
        }

        /// <summary>
        /// Restituisce l'immagine corretta per lo stato 
        /// </summary>
        /// <returns></returns>
        private Image GetCorrectStatusImage()
        {
            if (state) return activeImage;
            else return notActiveImage;
        }

        /// <summary>
        /// Reset dello stato
        /// </summary>
        public void ResetStatus()
        {
            state = false;
            ChangeObjectColor(true);
            ChangeObjectImage();
        }

        /// <summary>
        /// Cambia il colore al pulsante
        /// </summary>
        private void ChangeObjectColor(bool useDefaultColor)
        {
            if (!useColors) return;
            if (_button == null || _button.IsDisposed) return;

            Color colorToUse;

            if (useDefaultColor)
                 colorToUse = GetCorrectStatusDefaultColor();
            else // Se è stato richiesto un colore custom
                 colorToUse = GetCorrectStatusCustomColor();

            if (_button.InvokeRequired)
            {
                _button.BeginInvoke(new MethodInvoker(() =>
                {
                    if (!_button.IsDisposed && _button.BackColor != colorToUse)
                    {
                        _button.BackColor = colorToUse;
                    }
                }));
            }
            else
            {
                _button.BackColor = colorToUse;
            }
        }

        /// <summary>
        /// Cambia il testo al pulsante
        /// </summary>
        private void ChangeObjectText()
        {
            if (!useColors) return;
            if (_button == null || _button.IsDisposed) return;

            string textToUse = GetCorrectText();

            if (_button.InvokeRequired)
            {
                _button.BeginInvoke(new MethodInvoker(() =>
                {
                    if (!_button.IsDisposed && _button.Text != textToUse)
                    {
                        _button.Text = textToUse;
                    }
                }));
            }
            else
            {
                _button.Text = textToUse;
            }
        }

        /// <summary>
        /// Cambia l'immagine di background al pulsante
        /// </summary>
        private void ChangeObjectImage()
        {
            if (!useImages) return;
            if (_button == null || _button.IsDisposed) return;

            Image imageToUse = GetCorrectStatusImage();

            if (_button.InvokeRequired)
            {
                _button.BeginInvoke(new MethodInvoker(() =>
                {
                    if (!_button.IsDisposed && _button.BackgroundImage != imageToUse)
                    {
                        _button.BackgroundImage = imageToUse;
                    }
                }));
            }
            else
            {
                _button.BackgroundImage = imageToUse;
            }
        }

        /// <summary>
        /// Cambia il testo al pulsante
        /// </summary>
        /// <param name="text"></param>
        private void SetText(string text)
        {
            if (_button == null || _button.IsDisposed) return;

            if (_button.InvokeRequired)
            {
                _button.BeginInvoke(new MethodInvoker(() =>
                {
                    if (!_button.IsDisposed && _button.Text != text)
                    {
                        _button.Text = text;
                    }
                }));
            }
            else
            {
                _button.Text = text;
            }
        }

        /// <summary>
        /// Cambia l'immagine di background al pulsante
        /// </summary>
        /// <param name="image"></param>
        private void SetBackImage(Image image)
        {
            if (_button == null || _button.IsDisposed) return;

            if (_button.InvokeRequired)
            {
                _button.BeginInvoke(new MethodInvoker(() =>
                {
                    if (!_button.IsDisposed && _button.BackgroundImage != image)
                    {
                        _button.BackgroundImage = image;
                    }
                }));
            }
            else
            {
                _button.BackgroundImage = image;
            }
        }

        /// <summary>
        /// Legge lo stato del pulsante
        /// </summary>
        /// <returns></returns>
        public bool ReadState()
        {
            return state;
        }

        #endregion
    }
}
