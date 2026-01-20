
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

        public static void ParsePlcVariableName(
    string fullName,
    out string axe,
    out string variableName)
        {
            axe = string.Empty;
            variableName = string.Empty;

            if (string.IsNullOrWhiteSpace(fullName))
                return;

            var firstUnderscore = fullName.IndexOf('_');
            if (firstUnderscore < 0)
                return;

            var secondUnderscore = fullName.IndexOf('_', firstUnderscore + 1);
            if (secondUnderscore < 0)
                return;

            axe = fullName.Substring(
                firstUnderscore + 1,
                secondUnderscore - firstUnderscore - 1);

            variableName = fullName.Substring(secondUnderscore + 1);
        }

    }
}
