
namespace RM.src.RM220930.Classes.PLC
{
    /// <summary>
    /// Contiene i tag degli indirizzi PLC
    /// </summary>
    public class PLCTagName
    {

        /// <summary>
        /// Clock da scrivere ogni secondo (il clock su PLC gira ogni 2s)
        /// </summary>
        public const string LifeBit_out = "PLC1_" + "com_robot_1";

        /// <summary>
        /// Clock da leggere ogni secondo (su clock su PLC gira ogni 2s)
        /// </summary>
        public const string LifeBit_in = "PLC1_" + "com_robot_152";

        #region axeUDT_bool

        /// <summary>
        /// 
        /// </summary>
        public const string Cmd_On_Axe = "array_bool_1";

        /// <summary>
        /// 
        /// </summary>
        public const string Cmd_En_Axe = "array_bool_2";

        /// <summary>
        /// 
        /// </summary>
        public const string Cmd_Go_home = "array_bool_3";

        /// <summary>
        /// 
        /// </summary>
        public const string Cmd_AutoFrom_Pc = "array_bool_4";

        /// <summary>
        /// 
        /// </summary>
        public const string Cmd_Auto = "array_bool_5";

        /// <summary>
        /// 
        /// </summary>
        public const string Cmd_Rif_Chain = "array_bool_6";

        /// <summary>
        /// 
        /// </summary>
        public const string Cmd_move = "array_bool_7";

        /// <summary>
        /// 
        /// </summary>
        public const string Cmd_Start_Pos = "array_bool_8";

        /// <summary>
        /// 
        /// </summary>
        public const string Cmd_Jog_Pos = "array_bool_9";

        /// <summary>
        /// 
        /// </summary>
        public const string Cmd_Jog_neg = "array_bool_10";

        /// <summary>
        /// 
        /// </summary>
        public const string Cmd_Abs_Mode = "array_bool_11";

        /// <summary>
        /// 
        /// </summary>
        public const string Cmd_Enable_Fifo = "array_bool_12";

        /// <summary>
        /// 
        /// </summary>
        public const string Cmd_Enable_Grouppo = "array_bool_13";

        /// <summary>
        /// 
        /// </summary>
        public const string Cmd_Stop_Axe = "array_bool_14";

        /// <summary>
        /// 
        /// </summary>
        public const string Cmd_Start_Cam_Table = "array_bool_15";

        /// <summary>
        /// 
        /// </summary>
        public const string Cmd_Start_Cam = "array_bool_16";

        /// <summary>
        /// 
        /// </summary>
        public const string Cmd_Stop_Cam = "array_bool_17";

        /// <summary>
        /// 
        /// </summary>
        public const string Read_No_Piece = "array_bool_18";

        /// <summary>
        /// 
        /// </summary>
        public const string Read_Error = "array_bool_19";

        /// <summary>
        /// 
        /// </summary>
        public const string Read_Axe_Power_On = "array_bool_20";

        /// <summary>
        /// 
        /// </summary>
        public const string Read_Ls_Pos = "array_bool_21";

        /// <summary>
        /// 
        /// </summary>
        public const string Read_Ls_Neg = "array_bool_22";

        /// <summary>
        /// 
        /// </summary>
        public const string Read_Bit_Min = "array_bool_23";

        /// <summary>
        /// 
        /// </summary>
        public const string Read_Home_Ok = "array_bool_24";

        /// <summary>
        /// 
        /// </summary>
        public const string Read_In_Pos = "array_bool_25";

        /// <summary>
        /// 
        /// </summary>
        public const string Read_Positioning_in_Prog = "array_bool_26";

        /// <summary>
        /// 
        /// </summary>
        public const string Read_Jog_in_Prog = "array_bool_27";

        /// <summary>
        /// 
        /// </summary>
        public const string Read_Timeout_Home = "array_bool_28";

        /// <summary>
        /// 
        /// </summary>
        public const string Read_Home_In_Prog = "array_bool_29";

        #endregion

        #region axeUDT_int16

        /// <summary>
        /// 
        /// </summary>
        public const string Cmd_Time_Home = "array_int_1";

        /// <summary>
        /// 
        /// </summary>
        public const string Cmd_From = "array_int_2";

        /// <summary>
        /// 
        /// </summary>
        public const string Cmd_For = "array_int_3";

        /// <summary>
        /// 
        /// </summary>
        public const string Cmd_Axe_Mod = "array_int_4";

        /// <summary>
        /// 
        /// </summary>
        public const string Cmd_Distance = "array_int_5";

        /// <summary>
        /// 
        /// </summary>
        public const string Cmd_Advance = "array_int_6";

        /// <summary>
        /// 
        /// </summary>
        public const string Cmd_Delay = "array_int_7";

        /// <summary>
        /// 
        /// </summary>
        public const string Cmd_Start_Group = "array_int_8";

        /// <summary>
        /// 
        /// </summary>
        public const string Cmd_End_Group = "array_int_9";

        /// <summary>
        /// 
        /// </summary>
        public const string Cmd_Bar_Group = "array_int_10";

        /// <summary>
        /// 
        /// </summary>
        public const string Cmd_Minimim = "array_int_11";

        /// <summary>
        /// 
        /// </summary>
        public const string Cmd_Maximum = "array_int_12";

        #endregion

        #region axeUDT_float

        /// <summary>
        /// 
        /// </summary>
        public const string Cmd_Acc = "array_float_1";

        /// <summary>
        /// 
        /// </summary>
        public const string Cmd_Dec = "array_float_2";

        /// <summary>
        /// 
        /// </summary>
        public const string Cmd_K_Position = "array_float_3";

        /// <summary>
        /// 
        /// </summary>
        public const string Cmd_K_Speed = "array_float_4";

        /// <summary>
        /// 
        /// </summary>
        public const string Cmd_Min_Speed = "array_float_5";

        /// <summary>
        /// 
        /// </summary>
        public const string Cmd_Max_Speed = "array_float_6";

        /// <summary>
        /// 
        /// </summary>
        public const string Cmd_Home_Speed = "array_float_7";

        /// <summary>
        /// 
        /// </summary>
        public const string Cmd_Min_Pos = "array_float_8";

        /// <summary>
        /// 
        /// </summary>
        public const string Cmd_Max_Pos = "array_float_9";

        /// <summary>
        /// 
        /// </summary>
        public const string Cmd_Preset_Home_Pos = "array_float_10";

        /// <summary>
        /// 
        /// </summary>
        public const string Cmd_Offset = "array_float_11";

        /// <summary>
        /// 
        /// </summary>
        public const string Cmd_No_Piece_Position = "array_float_12";

        /// <summary>
        /// 
        /// </summary>
        public const string Cmd_Up_Correction = "array_float_13";

        /// <summary>
        /// 
        /// </summary>
        public const string Cmd_Down_Correction = "array_float_14";

        /// <summary>
        /// 
        /// </summary>
        public const string Cmd_Pos_Range = "array_float_15";

        /// <summary>
        /// 
        /// </summary>
        public const string Cmd_Pos_Window = "array_float_16";

        /// <summary>
        /// 
        /// </summary>
        public const string Cmd_Calculate_Pos = "array_float_17";

        /// <summary>
        /// 
        /// </summary>
        public const string Cmd_Stop_Pos = "array_float_18";

        /// <summary>
        /// 
        /// </summary>
        public const string Cmd_Up_Pos = "array_float_19";

        /// <summary>
        /// 
        /// </summary>
        public const string Cmd_Down_Pos = "array_float_20";

        /// <summary>
        /// 
        /// </summary>
        public const string Cmd_Stop_Wash_Pos = "array_float_21";

        /// <summary>
        /// 
        /// </summary>
        public const string Cmd_Offset_From_Piece = "array_float_22";

        /// <summary>
        /// 
        /// </summary>
        public const string Cmd_Distance_From_Center = "array_float_23";

        /// <summary>
        /// 
        /// </summary>
        public const string Cmd_Wash_Pos = "array_float_24";

        /// <summary>
        /// 
        /// </summary>
        public const string Cmd_Jog_Speed = "array_float_25";

        /// <summary>
        /// 
        /// </summary>
        public const string Cmd_Target_Pos = "array_float_26";

        /// <summary>
        /// 
        /// </summary>
        public const string Cmd_Speed_Pos = "array_float_27";

        /// <summary>
        /// 
        /// </summary>
        public const string offsetFromFloor = "array_float_28";

        /// <summary>
        /// 
        /// </summary>
        public const string Read_Act_Pos = "array_float_29";

        /// <summary>
        /// 
        /// </summary>
        public const string Read_Act_Speed = "array_float_30";

        #endregion

        #region GENERAL

        #region BOOL
        /// <summary>
        /// Allarme 01 Emergenza
        /// </summary>
        public const string Alm001_Emergenza = "array_general_bool_1";
        /// <summary>
        /// Allarme 02
        /// </summary>
        public const string Alm002 = "array_general_bool_2";
        /// <summary>
        /// Allarme 03
        /// </summary>
        public const string Alm003 = "array_general_bool_3";
        /// <summary>
        /// Allarme 04
        /// </summary>
        public const string Alm004 = "array_general_bool_4";
        /// <summary>
        /// Allarme 05
        /// </summary>
        public const string Alm005 = "array_general_bool_5";
        /// <summary>
        /// Allarme 06
        /// </summary>
        public const string Alm006 = "array_general_bool_6";
        /// <summary>
        /// Allarme 07
        /// </summary>
        public const string Alm007 = "array_general_bool_7";
        /// <summary>
        /// Allarme 08
        /// </summary>
        public const string Alm008 = "array_general_bool_8";
        /// <summary>
        /// Allarme 09
        /// </summary>
        public const string Alm009 = "array_general_bool_9";
        /// <summary>
        /// Allarme 10 Errore Rec
        /// </summary>
        public const string Alm010_Error_Rec = "array_general_bool_10";
        /// <summary>
        /// Allarme 11 Errore Z1
        /// </summary>
        public const string Alm011_Error_Z1 = "array_general_bool_11";
        /// <summary>
        /// Allarme 12 Errore Z2
        /// </summary>
        public const string Alm012_Error_Z2 = "array_general_bool_12";
        /// <summary>
        /// Allarme 13 Errore Z3
        /// </summary>
        public const string Alm013_Error_Z3 = "array_general_bool_13";
        /// <summary>
        /// Allarme 14 Errore Z4
        /// </summary>
        public const string Alm014_Error_Z4 = "array_general_bool_14";
        /// <summary>
        /// Allarme 15 Errore Z5
        /// </summary>
        public const string Alm015_Error_Z5 = "array_general_bool_15";
        /// <summary>
        /// Allarme 16 Errore Z6
        /// </summary>
        public const string Alm016_Error_Z6 = "array_general_bool_16";
        /// <summary>
        /// Allarme 17 Errore Z7
        /// </summary>
        public const string Alm017_Error_Z7 = "array_general_bool_17";
        /// <summary>
        /// Allarme 18 Errore Z8
        /// </summary>
        public const string Alm018_Error_Z8 = "array_general_bool_18";
        /// <summary>
        /// Allarme 19
        /// </summary>
        public const string Alm019 = "array_general_bool_19";
        /// <summary>
        /// Allarme 20
        /// </summary>
        public const string Alm020 = "array_general_bool_20";
        /// <summary>
        /// Allarme 21
        /// </summary>
        public const string Alm021 = "array_general_bool_21";
        /// <summary>
        /// Allarme 22
        /// </summary>
        public const string Alm022 = "array_general_bool_22";
        /// <summary>
        /// Allarme 23
        /// </summary>
        public const string Alm023 = "array_general_bool_23";
        /// <summary>
        /// Allarme 24
        /// </summary>
        public const string Alm024 = "array_general_bool_24";
        /// <summary>
        /// Allarme 25
        /// </summary>
        public const string Alm025 = "array_general_bool_25";
        /// <summary>
        /// Allarme 26 Timeout com laser
        /// </summary>
        public const string Alm026_Timeout_Com_Laser = "array_general_bool_26";
        /// <summary>
        /// Allarme 27 Laser Error Fault 1
        /// </summary>
        public const string Alm027_Laser_Error_Fault_1 = "array_general_bool_27";
        /// <summary>
        /// Allarme 28 Laser Error Fault 2
        /// </summary>
        public const string Alm028_Laser_Error_Fault_2 = "array_general_bool_28";
        /// <summary>
        /// Allarme 29 Waring
        /// </summary>
        public const string Alm029_Warning = "array_general_bool_29";
        /// <summary>
        /// Allarme 30 Errore Lettura Laser
        /// </summary>
        public const string Alm030_ErroreLetturaLaser = "array_general_bool_30";
        /// <summary>
        /// Allarme 31 Errore Scrittura Laser
        /// </summary>
        public const string Alm031_ErroreScritturaLaser = "array_general_bool_31";
        /// <summary>
        /// Allarme 32 Errore Client Laser
        /// </summary>
        public const string Alm032_ErrorClientLaser = "array_general_bool_32";
        /// <summary>
        /// Reset di tutti gli allarmi
        /// </summary>
        public const string Hmi_Reset = "array_general_bool_33";
        /// <summary>
        /// Impostazione dell'impianto in manuale
        /// </summary>
        public const string Hmi_Select_Manual = "array_general_bool_34";
        /// <summary>
        /// Impostazione dell'impianto in automatico
        /// </summary>
        public const string Hmi_Select_Automatic = "array_general_bool_35";
        /// <summary>
        /// A true quando l'impianto è in automatico
        /// </summary>
        public const string Hmi_Vis_Automatic_Mode = "array_general_bool_36";
        /// <summary>
        /// A true quando l'impianto è in manuale
        /// </summary>
        public const string Hmi_Vis_Manual_Mode = "array_general_bool_37";
        /// <summary>
        /// A true quando vengono resettate modalità di auto e manual
        /// </summary>
        public const string Hmi_Vis_Pos_0 = "array_general_bool_38";
        /// <summary>
        /// A true quando tutti gli assi in home
        /// </summary>
        public const string Hmi_Read_All_Axis_Home_Ok = "array_general_bool_39";
        /// <summary>
        /// Invio in home di tutti gli assi
        /// </summary>
        public const string Hmi_Cmd_Go_Home_All_Axis = "array_general_bool_40";
        /// <summary>
        /// Tutti gli assi ON
        /// </summary>
        public const string Hmi_Cmd_All_Axis_In_Power = "array_general_bool_41";

        #endregion

        #endregion

    }
}
