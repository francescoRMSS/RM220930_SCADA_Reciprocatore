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
        /// <summary>
        /// Posizione alta
        /// </summary>
        public float CmdMaxPos { get; set; }
        /// <summary>
        /// Posizione bassa
        /// </summary>
        public float CmdMinPos { get; set; }
        /// <summary>
        /// Vel min
        /// </summary>
        public float CmdVelMin { get; set; }
        /// <summary>
        /// Vel max
        /// </summary>
        public float CmdVelMax { get; set; }
        /// <summary>
        /// Acceleration
        /// </summary>
        public float CmdAcc { get; set; }
        /// <summary>
        /// Deceleration
        /// </summary>
        public float CmdDec { get; set; }
        /// <summary>
        /// Pos stop
        /// </summary>
        public float cmdStopPos { get; set; }
        /// <summary>
        /// Vel stop
        /// </summary>
        public float CmdVelStop { get; set; }

        /// <summary>
        /// Offset base
        /// </summary>
        public float CmdOffset { get; set; }
        /// <summary>
        /// Distanza pistole
        /// </summary>
        public float CmdDistanceFromCenter{ get; set; }
        /// <summary>
        /// Pos lavaggio
        /// </summary>
        public float CmdWashPos { get; set; }

        #endregion

        #region Integer Variables (INT / short)
        /// <summary>
        /// Codice di errore dell'asse letto dal PLC.
        /// 0 = nessun errore.
        /// </summary>
        public short ErrorCode { get; set; }
        /// <summary>
        /// Home timeout
        /// </summary>
        public int CmdHomeTimeout { get; set; }
        /// <summary>
        /// Delay
        /// </summary>
        public int CmdDelay { get; set; }
        /// <summary>
        /// Advance
        /// </summary>
        public int CmdAdvance { get; set; }
        /// <summary>
        /// Distance
        /// </summary>
        public int CmdDistance { get; set; }

        /// <summary>
        /// Vel lavaggio
        /// </summary>
        public int VelLavaggio { get; set; }
        #endregion
    }

}
