using System.Collections.Generic;

namespace RM.src.RM220930.Classes.PLC
{
    public static class PlcTagMap
    {
        public static readonly IReadOnlyDictionary<string, string> PlcToApp =
            new Dictionary<string, string>
            {
                // axeUDT_bool
                { PLCTagName.Cmd_On_Axe, nameof(PLCTagName.Cmd_On_Axe) },
                { PLCTagName.Cmd_En_Axe, nameof(PLCTagName.Cmd_En_Axe) },
                { PLCTagName.Cmd_Go_home, nameof(PLCTagName.Cmd_Go_home) },
                { PLCTagName.Cmd_AutoFrom_Pc, nameof(PLCTagName.Cmd_AutoFrom_Pc) },
                { PLCTagName.Cmd_Auto, nameof(PLCTagName.Cmd_Auto) },
                { PLCTagName.Cmd_Rif_Chain, nameof(PLCTagName.Cmd_Rif_Chain) },
                { PLCTagName.Cmd_move, nameof(PLCTagName.Cmd_move) },
                { PLCTagName.Cmd_Start_Pos, nameof(PLCTagName.Cmd_Start_Pos) },
                { PLCTagName.Cmd_Jog_Pos, nameof(PLCTagName.Cmd_Jog_Pos) },
                { PLCTagName.Cmd_Jog_neg, nameof(PLCTagName.Cmd_Jog_neg) },
                { PLCTagName.Cmd_Abs_Mode, nameof(PLCTagName.Cmd_Abs_Mode) },
                { PLCTagName.Cmd_Enable_Fifo, nameof(PLCTagName.Cmd_Enable_Fifo) },
                { PLCTagName.Cmd_Enable_Grouppo, nameof(PLCTagName.Cmd_Enable_Grouppo) },
                { PLCTagName.Cmd_Stop_Axe, nameof(PLCTagName.Cmd_Stop_Axe) },
                { PLCTagName.Cmd_Start_Cam_Table, nameof(PLCTagName.Cmd_Start_Cam_Table) },
                { PLCTagName.Cmd_Start_Cam, nameof(PLCTagName.Cmd_Start_Cam) },
                { PLCTagName.Cmd_Stop_Cam, nameof(PLCTagName.Cmd_Stop_Cam) },
                { PLCTagName.Read_No_Piece, nameof(PLCTagName.Read_No_Piece) },
                { PLCTagName.Read_Error, nameof(PLCTagName.Read_Error) },
                { PLCTagName.Read_Axe_Power_On, nameof(PLCTagName.Read_Axe_Power_On) },
                { PLCTagName.Read_Ls_Pos, nameof(PLCTagName.Read_Ls_Pos) },
                { PLCTagName.Read_Ls_Neg, nameof(PLCTagName.Read_Ls_Neg) },
                { PLCTagName.Read_Bit_Min, nameof(PLCTagName.Read_Bit_Min) },
                { PLCTagName.Read_Home_Ok, nameof(PLCTagName.Read_Home_Ok) },
                { PLCTagName.Read_In_Pos, nameof(PLCTagName.Read_In_Pos) },
                { PLCTagName.Read_Positioning_in_Prog, nameof(PLCTagName.Read_Positioning_in_Prog) },
                { PLCTagName.Read_Jog_in_Prog, nameof(PLCTagName.Read_Jog_in_Prog) },
                { PLCTagName.Read_Timeout_Home, nameof(PLCTagName.Read_Timeout_Home) },
                { PLCTagName.Read_Home_In_Prog, nameof(PLCTagName.Read_Home_In_Prog) },

                // axeUDT_int16
                { PLCTagName.Cmd_Time_Home, nameof(PLCTagName.Cmd_Time_Home) },
                { PLCTagName.Cmd_From, nameof(PLCTagName.Cmd_From) },
                { PLCTagName.Cmd_For, nameof(PLCTagName.Cmd_For) },
                { PLCTagName.Cmd_Axe_Mod, nameof(PLCTagName.Cmd_Axe_Mod) },
                { PLCTagName.Cmd_Distance, nameof(PLCTagName.Cmd_Distance) },
                { PLCTagName.Cmd_Advance, nameof(PLCTagName.Cmd_Advance) },
                { PLCTagName.Cmd_Delay, nameof(PLCTagName.Cmd_Delay) },
                { PLCTagName.Cmd_Start_Group, nameof(PLCTagName.Cmd_Start_Group) },
                { PLCTagName.Cmd_End_Group, nameof(PLCTagName.Cmd_End_Group) },
                { PLCTagName.Cmd_Bar_Group, nameof(PLCTagName.Cmd_Bar_Group) },
                { PLCTagName.Cmd_Minimim, nameof(PLCTagName.Cmd_Minimim) },
                { PLCTagName.Cmd_Maximum, nameof(PLCTagName.Cmd_Maximum) },

                // axeUDT_float
                { PLCTagName.Cmd_Acc, nameof(PLCTagName.Cmd_Acc) },
                { PLCTagName.Cmd_Dec, nameof(PLCTagName.Cmd_Dec) },
                { PLCTagName.Cmd_K_Position, nameof(PLCTagName.Cmd_K_Position) },
                { PLCTagName.Cmd_K_Speed, nameof(PLCTagName.Cmd_K_Speed) },
                { PLCTagName.Cmd_Min_Speed, nameof(PLCTagName.Cmd_Min_Speed) },
                { PLCTagName.Cmd_Max_Speed, nameof(PLCTagName.Cmd_Max_Speed) },
                { PLCTagName.Cmd_Home_Speed, nameof(PLCTagName.Cmd_Home_Speed) },
                { PLCTagName.Cmd_Min_Pos, nameof(PLCTagName.Cmd_Min_Pos) },
                { PLCTagName.Cmd_Max_Pos, nameof(PLCTagName.Cmd_Max_Pos) },
                { PLCTagName.Cmd_Preset_Home_Pos, nameof(PLCTagName.Cmd_Preset_Home_Pos) },
                { PLCTagName.Cmd_Offset, nameof(PLCTagName.Cmd_Offset) },
                { PLCTagName.Cmd_No_Piece_Position, nameof(PLCTagName.Cmd_No_Piece_Position) },
                { PLCTagName.Cmd_Up_Correction, nameof(PLCTagName.Cmd_Up_Correction) },
                { PLCTagName.Cmd_Down_Correction, nameof(PLCTagName.Cmd_Down_Correction) },
                { PLCTagName.Cmd_Pos_Range, nameof(PLCTagName.Cmd_Pos_Range) },
                { PLCTagName.Cmd_Pos_Window, nameof(PLCTagName.Cmd_Pos_Window) },
                { PLCTagName.Cmd_Calculate_Pos, nameof(PLCTagName.Cmd_Calculate_Pos) },
                { PLCTagName.Cmd_Stop_Pos, nameof(PLCTagName.Cmd_Stop_Pos) },
                { PLCTagName.Cmd_Up_Pos, nameof(PLCTagName.Cmd_Up_Pos) },
                { PLCTagName.Cmd_Down_Pos, nameof(PLCTagName.Cmd_Down_Pos) },
                { PLCTagName.Cmd_Stop_Wash_Pos, nameof(PLCTagName.Cmd_Stop_Wash_Pos) },
                { PLCTagName.Cmd_Offset_From_Piece, nameof(PLCTagName.Cmd_Offset_From_Piece) },
                { PLCTagName.Cmd_Distance_From_Center, nameof(PLCTagName.Cmd_Distance_From_Center) },
                { PLCTagName.Cmd_Wash_Pos, nameof(PLCTagName.Cmd_Wash_Pos) },
                { PLCTagName.Cmd_Jog_Speed, nameof(PLCTagName.Cmd_Jog_Speed) },
                { PLCTagName.Cmd_Target_Pos, nameof(PLCTagName.Cmd_Target_Pos) },
                { PLCTagName.Cmd_Speed_Pos, nameof(PLCTagName.Cmd_Speed_Pos) },
                { PLCTagName.offsetFromFloor, nameof(PLCTagName.offsetFromFloor) },
                { PLCTagName.Read_Act_Pos, nameof(PLCTagName.Read_Act_Pos) },
                { PLCTagName.Read_Act_Speed, nameof(PLCTagName.Read_Act_Speed) },
            };
    }
}
