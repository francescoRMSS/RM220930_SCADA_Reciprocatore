using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RM.src.RM220930.Classes
{
    /// <summary>
    /// Rappresenta lo stato di un asse Z del robot/PLC.
    /// Contiene le principali variabili lette dal PLC per ciascun asse.
    /// </summary>
    public class ZAxisState
    {
        #region Boolean Variables (BOOL)
        /// <summary>
        /// Comando ON/OFF dell'asse letto dal PLC.
        /// True = asse attivo, False = asse spento.
        /// </summary>
        public bool CmdOnAxe { get; set; }
        /// <summary>
        /// Comando abilita/disabilita dell'asse letto dal PLC.
        /// True = asse attivo, False = asse spento.
        /// </summary>
        public bool CmdEnAxe { get; set; }
        /// <summary>
        /// Feedback ritorno in Home
        /// </summary>
        public bool ReadHomeOK { get; set; }
        /// <summary>
        /// Comando AUTO ON/OFF
        /// </summary>
        public bool CmdAutoFromPc { get; set; }
        /// <summary>
        /// Comando modalità manual HMI
        /// </summary>
        public bool HmiSelectManual { get; set; }
        #endregion

        #region Floating Point Variables (REAL / float)
        /// <summary>
        /// Posizione attuale dell'asse letta dal PLC.
        /// </summary>
        public float ActPosition { get; set; }
        /// <summary>
        /// Velocità asse
        /// </summary>
        public float cmdSpeedPos { get; set; }
        /// <summary>
        /// Posizione no pezzo asse
        /// </summary>
        public float CmdPosRange { get; set; }
        /// <summary>
        /// Distanza dal pezzo
        /// </summary>
        public float CmdOffsetFromPiece { get; set; }
        #endregion

        #region Integer Variables (INT / short)
        /// <summary>
        /// Codice di errore dell'asse letto dal PLC.
        /// 0 = nessun errore.
        /// </summary>
        public short ErrorCode { get; set; }
        #endregion
    }

}
