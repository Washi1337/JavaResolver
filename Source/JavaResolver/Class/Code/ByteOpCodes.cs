namespace JavaResolver.Class.Code
{
    /// <summary>
    /// Represents a collection of all possible bytecode opcodes.
    /// </summary>
    public static class ByteOpCodes
    {
        /// <summary>
        /// Contains all opcodes in a single array. The array is sorted in such a way that the index of each element
        /// is the same as the actual raw byte of the opcode, allowing for easy lookup of opcodes.
        /// </summary>
        // Array is populated using the internal ByteOpCode constructor.§
        public static readonly ByteOpCode[] All = new ByteOpCode[256];

        public static readonly ByteOpCode Nop = new ByteOpCode(ByteCode.Nop, ((byte) ByteCodeFlowControl.Next << 24)
                                                                             | ((byte) ByteCodeStackBehaviour.None << 16)
                                                                             | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                             | (byte) ByteCodeOperandType.None);
        
        public static readonly ByteOpCode AConst_Null = new ByteOpCode(ByteCode.AConst_Null, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                             | ((byte) ByteCodeStackBehaviour.None << 16)
                                                                                             | ((byte) ByteCodeStackBehaviour.PushRef << 8)
                                                                                             | (byte) ByteCodeOperandType.None);
        
        public static readonly ByteOpCode IConst_M1 = new ByteOpCode(ByteCode.IConst_M1, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                         | ((byte) ByteCodeStackBehaviour.None << 16)
                                                                                         | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                                         | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode IConst_0 = new ByteOpCode(ByteCode.IConst_0, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                       | ((byte) ByteCodeStackBehaviour.None << 16)
                                                                                       | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                                       | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode IConst_1 = new ByteOpCode(ByteCode.IConst_1, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                       | ((byte) ByteCodeStackBehaviour.None << 16)
                                                                                       | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                                       | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode IConst_2 = new ByteOpCode(ByteCode.IConst_2, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                       | ((byte) ByteCodeStackBehaviour.None << 16)
                                                                                         | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                                         | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode IConst_3 = new ByteOpCode(ByteCode.IConst_3, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                       | ((byte) ByteCodeStackBehaviour.None << 16)
                                                                                         | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                                         | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode IConst_4 = new ByteOpCode(ByteCode.IConst_4, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                       | ((byte) ByteCodeStackBehaviour.None << 16)
                                                                                         | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                                         | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode IConst_5 = new ByteOpCode(ByteCode.IConst_5, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                       | ((byte) ByteCodeStackBehaviour.None << 16)
                                                                                         | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                                         | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode LConst_0 = new ByteOpCode(ByteCode.LConst_0, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                       | ((byte) ByteCodeStackBehaviour.None << 16)
                                                                                         | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                                         | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode LConst_1 = new ByteOpCode(ByteCode.LConst_1, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                       | ((byte) ByteCodeStackBehaviour.None << 16)
                                                                                         | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                                         | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode FConst_0 = new ByteOpCode(ByteCode.FConst_0, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                       | ((byte) ByteCodeStackBehaviour.None << 16)
                                                                                         | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                                         | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode FConst_1 = new ByteOpCode(ByteCode.FConst_1, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                       | ((byte) ByteCodeStackBehaviour.None << 16)
                                                                                         | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                                         | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode FConst_2 = new ByteOpCode(ByteCode.FConst_2, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                       | ((byte) ByteCodeStackBehaviour.None << 16)
                                                                                         | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                                         | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode DConst_0 = new ByteOpCode(ByteCode.DConst_0, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                       | ((byte) ByteCodeStackBehaviour.None << 16)
                                                                                         | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                                         | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode DConst_1 = new ByteOpCode(ByteCode.DConst_1, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                       | ((byte) ByteCodeStackBehaviour.None << 16)
                                                                                         | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                                         | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode BiPush = new ByteOpCode(ByteCode.BiPush, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                   | ((byte) ByteCodeStackBehaviour.None << 16)
                                                                                   | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                                   | (byte) ByteCodeOperandType.Byte);
        public static readonly ByteOpCode SiPush = new ByteOpCode(ByteCode.SiPush, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                   | ((byte) ByteCodeStackBehaviour.None << 16)
                                                                                   | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                                   | (byte) ByteCodeOperandType.Short);
        public static readonly ByteOpCode Ldc = new ByteOpCode(ByteCode.Ldc, ((byte) ByteCodeFlowControl.Next << 24)
                                                                             | ((byte) ByteCodeStackBehaviour.None << 16)
                                                                             | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                             | (byte) ByteCodeOperandType.ConstantIndex);
        public static readonly ByteOpCode Ldc_w = new ByteOpCode(ByteCode.Ldc_w, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                 | ((byte) ByteCodeStackBehaviour.None << 16)
                                                                                 | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                                 | (byte) ByteCodeOperandType.WideConstantIndex);
        public static readonly ByteOpCode Ldc2_w = new ByteOpCode(ByteCode.Ldc2_w, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                   | ((byte) ByteCodeStackBehaviour.None << 16)
                                                                                   | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                                   | (byte) ByteCodeOperandType.WideConstantIndex);
        public static readonly ByteOpCode ILoad = new ByteOpCode(ByteCode.ILoad, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                 | ((byte) ByteCodeStackBehaviour.None << 16)
                                                                                 | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                                 | (byte) ByteCodeOperandType.LocalIndex);
        public static readonly ByteOpCode LLoad = new ByteOpCode(ByteCode.LLoad, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                 | ((byte) ByteCodeStackBehaviour.None << 16)
                                                                                 | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                                 | (byte) ByteCodeOperandType.LocalIndex);
        public static readonly ByteOpCode FLoad = new ByteOpCode(ByteCode.FLoad, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                 | ((byte) ByteCodeStackBehaviour.None << 16)
                                                                                 | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                                 | (byte) ByteCodeOperandType.LocalIndex);
        public static readonly ByteOpCode DLoad = new ByteOpCode(ByteCode.DLoad, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                 | ((byte) ByteCodeStackBehaviour.None << 16)
                                                                                 | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                                 | (byte) ByteCodeOperandType.LocalIndex);
        public static readonly ByteOpCode ALoad = new ByteOpCode(ByteCode.ALoad, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                 | ((byte) ByteCodeStackBehaviour.None << 16)
                                                                                 | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                                 | (byte) ByteCodeOperandType.LocalIndex);
        public static readonly ByteOpCode ILoad_0 = new ByteOpCode(ByteCode.ILoad_0, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                     | ((byte) ByteCodeStackBehaviour.None << 16)
                                                                                     | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                                     | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode ILoad_1 = new ByteOpCode(ByteCode.ILoad_1, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                     | ((byte) ByteCodeStackBehaviour.None << 16)
                                                                                     | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                                     | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode ILoad_2 = new ByteOpCode(ByteCode.ILoad_2, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                     | ((byte) ByteCodeStackBehaviour.None << 16)
                                                                                     | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                                     | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode ILoad_3 = new ByteOpCode(ByteCode.ILoad_3, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                     | ((byte) ByteCodeStackBehaviour.None << 16)
                                                                                     | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                                     | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode LLoad_0 = new ByteOpCode(ByteCode.LLoad_0, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                     | ((byte) ByteCodeStackBehaviour.None << 16)
                                                                                     | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                                     | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode LLoad_1 = new ByteOpCode(ByteCode.LLoad_1, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                     | ((byte) ByteCodeStackBehaviour.None << 16)
                                                                                     | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                                     | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode LLoad_2 = new ByteOpCode(ByteCode.LLoad_2, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                     | ((byte) ByteCodeStackBehaviour.None << 16)
                                                                                     | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                                     | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode LLoad_3 = new ByteOpCode(ByteCode.LLoad_3, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                     | ((byte) ByteCodeStackBehaviour.None << 16)
                                                                                     | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                                     | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode FLoad_0 = new ByteOpCode(ByteCode.FLoad_0, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                     | ((byte) ByteCodeStackBehaviour.None << 16)
                                                                                     | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                                     | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode FLoad_1 = new ByteOpCode(ByteCode.FLoad_1, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                     | ((byte) ByteCodeStackBehaviour.None << 16)
                                                                                     | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                                     | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode FLoad_2 = new ByteOpCode(ByteCode.FLoad_2, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                     | ((byte) ByteCodeStackBehaviour.None << 16)
                                                                                     | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                                     | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode FLoad_3 = new ByteOpCode(ByteCode.FLoad_3, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                     | ((byte) ByteCodeStackBehaviour.None << 16)
                                                                                     | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                                     | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode DLoad_0 = new ByteOpCode(ByteCode.DLoad_0, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                     | ((byte) ByteCodeStackBehaviour.None << 16)
                                                                                     | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                                     | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode DLoad_1 = new ByteOpCode(ByteCode.DLoad_1, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                     | ((byte) ByteCodeStackBehaviour.None << 16)
                                                                                     | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                                     | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode DLoad_2 = new ByteOpCode(ByteCode.DLoad_2, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                     | ((byte) ByteCodeStackBehaviour.None << 16)
                                                                                     | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                                     | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode DLoad_3 = new ByteOpCode(ByteCode.DLoad_3, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                     | ((byte) ByteCodeStackBehaviour.None << 16)
                                                                                     | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                                     | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode ALoad_0 = new ByteOpCode(ByteCode.ALoad_0, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                     | ((byte) ByteCodeStackBehaviour.None << 16)
                                                                                     | ((byte) ByteCodeStackBehaviour.PushRef << 8)
                                                                                     | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode ALoad_1 = new ByteOpCode(ByteCode.ALoad_1, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                     | ((byte) ByteCodeStackBehaviour.None << 16)
                                                                                     | ((byte) ByteCodeStackBehaviour.PushRef << 8)
                                                                                     | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode ALoad_2 = new ByteOpCode(ByteCode.ALoad_2, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                     | ((byte) ByteCodeStackBehaviour.None << 16)
                                                                                     | ((byte) ByteCodeStackBehaviour.PushRef << 8)
                                                                                     | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode ALoad_3 = new ByteOpCode(ByteCode.ALoad_3, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                     | ((byte) ByteCodeStackBehaviour.None << 16)
                                                                                     | ((byte) ByteCodeStackBehaviour.PushRef << 8)
                                                                                     | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode IaLoad = new ByteOpCode(ByteCode.IaLoad, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                   | ((byte) ByteCodeStackBehaviour.PopRefPopValue << 16)
                                                                                   | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                                   | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode LaLoad = new ByteOpCode(ByteCode.LaLoad, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                   | ((byte) ByteCodeStackBehaviour.PopRefPopValue << 16)
                                                                                   | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                                   | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode FaLoad = new ByteOpCode(ByteCode.FaLoad, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                   | ((byte) ByteCodeStackBehaviour.PopRefPopValue << 16)
                                                                                   | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                                   | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode DaLoad = new ByteOpCode(ByteCode.DaLoad, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                   | ((byte) ByteCodeStackBehaviour.PopRefPopValue << 16)
                                                                                   | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                                   | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode AaLoad = new ByteOpCode(ByteCode.AaLoad, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                   | ((byte) ByteCodeStackBehaviour.PopRefPopValue << 16)
                                                                                   | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                                   | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode BaLoad = new ByteOpCode(ByteCode.BaLoad, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                   | ((byte) ByteCodeStackBehaviour.PopRefPopValue << 16)
                                                                                   | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                                   | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode CaLoad = new ByteOpCode(ByteCode.CaLoad, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                   | ((byte) ByteCodeStackBehaviour.PopRefPopValue << 16)
                                                                                   | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                                   | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode SaLoad = new ByteOpCode(ByteCode.SaLoad, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                   | ((byte) ByteCodeStackBehaviour.PopRefPopValue << 16)
                                                                                   | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                                   | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode IStore = new ByteOpCode(ByteCode.IStore, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                   | ((byte) ByteCodeStackBehaviour.PopValue1 << 16)
                                                                                   | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                                   | (byte) ByteCodeOperandType.LocalIndex);
        public static readonly ByteOpCode LStore = new ByteOpCode(ByteCode.LStore, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                   | ((byte) ByteCodeStackBehaviour.PopValue1 << 16)
                                                                                   | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                                   | (byte) ByteCodeOperandType.LocalIndex);
        public static readonly ByteOpCode FStore = new ByteOpCode(ByteCode.FStore, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                   | ((byte) ByteCodeStackBehaviour.PopValue1 << 16)
                                                                                   | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                                   | (byte) ByteCodeOperandType.LocalIndex);
        public static readonly ByteOpCode DStore = new ByteOpCode(ByteCode.DStore, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                   | ((byte) ByteCodeStackBehaviour.PopValue1 << 16)
                                                                                   | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                                   | (byte) ByteCodeOperandType.LocalIndex);
        public static readonly ByteOpCode AStore = new ByteOpCode(ByteCode.AStore, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                   | ((byte) ByteCodeStackBehaviour.PopRef << 16)
                                                                                   | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                                   | (byte) ByteCodeOperandType.LocalIndex);
        public static readonly ByteOpCode IStore_0 = new ByteOpCode(ByteCode.IStore_0, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                       | ((byte) ByteCodeStackBehaviour.PopValue1 << 16)
                                                                                       | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                                       | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode IStore_1 = new ByteOpCode(ByteCode.IStore_1, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                       | ((byte) ByteCodeStackBehaviour.PopValue1 << 16)
                                                                                       | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                                       | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode IStore_2 = new ByteOpCode(ByteCode.IStore_2, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                       | ((byte) ByteCodeStackBehaviour.PopValue1 << 16)
                                                                                       | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                                       | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode IStore_3 = new ByteOpCode(ByteCode.IStore_3, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                       | ((byte) ByteCodeStackBehaviour.PopValue1 << 16)
                                                                                       | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                                       | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode LStore_0 = new ByteOpCode(ByteCode.LStore_0, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                       | ((byte) ByteCodeStackBehaviour.PopValue1 << 16)
                                                                                       | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                                       | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode LStore_1 = new ByteOpCode(ByteCode.LStore_1, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                       | ((byte) ByteCodeStackBehaviour.PopValue1 << 16)
                                                                                       | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                                       | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode LStore_2 = new ByteOpCode(ByteCode.LStore_2, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                       | ((byte) ByteCodeStackBehaviour.PopValue1 << 16)
                                                                                       | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                                       | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode LStore_3 = new ByteOpCode(ByteCode.LStore_3, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                       | ((byte) ByteCodeStackBehaviour.PopValue1 << 16)
                                                                                       | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                                       | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode FStore_0 = new ByteOpCode(ByteCode.FStore_0, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                       | ((byte) ByteCodeStackBehaviour.PopValue1 << 16)
                                                                                       | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                                       | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode FStore_1 = new ByteOpCode(ByteCode.FStore_1, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                       | ((byte) ByteCodeStackBehaviour.PopValue1 << 16)
                                                                                       | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                                       | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode FStore_2 = new ByteOpCode(ByteCode.FStore_2, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                       | ((byte) ByteCodeStackBehaviour.PopValue1 << 16)
                                                                                       | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                                       | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode FStore_3 = new ByteOpCode(ByteCode.FStore_3, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                       | ((byte) ByteCodeStackBehaviour.PopValue1 << 16)
                                                                                       | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                                       | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode DStore_0 = new ByteOpCode(ByteCode.DStore_0, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                       | ((byte) ByteCodeStackBehaviour.PopValue1 << 16)
                                                                                       | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                                       | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode DStore_1 = new ByteOpCode(ByteCode.DStore_1, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                       | ((byte) ByteCodeStackBehaviour.PopValue1 << 16)
                                                                                       | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                                       | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode DStore_2 = new ByteOpCode(ByteCode.DStore_2, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                       | ((byte) ByteCodeStackBehaviour.PopValue1 << 16)
                                                                                       | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                                       | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode DStore_3 = new ByteOpCode(ByteCode.DStore_3, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                       | ((byte) ByteCodeStackBehaviour.PopValue1 << 16)
                                                                                       | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                                       | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode AStore_0 = new ByteOpCode(ByteCode.AStore_0, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                       | ((byte) ByteCodeStackBehaviour.PopRef << 16)
                                                                                       | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                                       | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode AStore_1 = new ByteOpCode(ByteCode.AStore_1, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                       | ((byte) ByteCodeStackBehaviour.PopRef << 16)
                                                                                       | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                                       | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode AStore_2 = new ByteOpCode(ByteCode.AStore_2, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                       | ((byte) ByteCodeStackBehaviour.PopRef << 16)
                                                                                       | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                                       | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode AStore_3 = new ByteOpCode(ByteCode.AStore_3, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                       | ((byte) ByteCodeStackBehaviour.PopRef << 16)
                                                                                       | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                                       | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode IaStore = new ByteOpCode(ByteCode.IaStore, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                     | ((byte) ByteCodeStackBehaviour.PopRefPopValuePopValue << 16)
                                                                                     | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                                     | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode LaStore = new ByteOpCode(ByteCode.LaStore, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                     | ((byte) ByteCodeStackBehaviour.PopRefPopValuePopValue << 16)
                                                                                     | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                                     | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode FaStore = new ByteOpCode(ByteCode.FaStore, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                     | ((byte) ByteCodeStackBehaviour.PopRefPopValuePopValue << 16)
                                                                                     | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                                     | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode DaStore = new ByteOpCode(ByteCode.DaStore, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                     | ((byte) ByteCodeStackBehaviour.PopRefPopValuePopValue << 16)
                                                                                     | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                                     | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode AaStore = new ByteOpCode(ByteCode.AaStore, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                     | ((byte) ByteCodeStackBehaviour.PopRefPopValuePopValue << 16)
                                                                                     | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                                     | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode BaStore = new ByteOpCode(ByteCode.BaStore, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                     | ((byte) ByteCodeStackBehaviour.PopRefPopValuePopValue << 16)
                                                                                     | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                                     | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode CaStore = new ByteOpCode(ByteCode.CaStore, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                     | ((byte) ByteCodeStackBehaviour.PopRefPopValuePopValue << 16)
                                                                                     | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                                     | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode SaStore = new ByteOpCode(ByteCode.SaStore, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                     | ((byte) ByteCodeStackBehaviour.PopRefPopValuePopValue << 16)
                                                                                     | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                                     | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode Pop = new ByteOpCode(ByteCode.Pop, ((byte) ByteCodeFlowControl.Next << 24)
                                                                             | ((byte) ByteCodeStackBehaviour.PopValue1 << 16)
                                                                             | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                             | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode Pop2 = new ByteOpCode(ByteCode.Pop2, ((byte) ByteCodeFlowControl.Next << 24)
                                                                               | ((byte) ByteCodeStackBehaviour.PopValue2 << 16)
                                                                               | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                               | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode Dup = new ByteOpCode(ByteCode.Dup, ((byte) ByteCodeFlowControl.Next << 24)
                                                                             | ((byte) ByteCodeStackBehaviour.PopValue1 << 16)
                                                                             | ((byte) ByteCodeStackBehaviour.PushValue2 << 8)
                                                                             | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode Dup_x1 = new ByteOpCode(ByteCode.Dup_x1, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                   | ((byte) ByteCodeStackBehaviour.PopValue2 << 16)
                                                                                   | ((byte) ByteCodeStackBehaviour.PushValue3 << 8)
                                                                                   | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode Dup_x2 = new ByteOpCode(ByteCode.Dup_x2, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                   | ((byte) ByteCodeStackBehaviour.PopValue3 << 16)
                                                                                   | ((byte) ByteCodeStackBehaviour.PushValue4 << 8)
                                                                                   | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode Dup2 = new ByteOpCode(ByteCode.Dup2, ((byte) ByteCodeFlowControl.Next << 24)
                                                                               | ((byte) ByteCodeStackBehaviour.PopValue2 << 16)
                                                                               | ((byte) ByteCodeStackBehaviour.PushValue4 << 8)
                                                                               | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode Dup2_x1 = new ByteOpCode(ByteCode.Dup2_x1, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                     | ((byte) ByteCodeStackBehaviour.PopValue3 << 16)
                                                                                     | ((byte) ByteCodeStackBehaviour.PushValue5 << 8)
                                                                                     | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode Dup2_x2 = new ByteOpCode(ByteCode.Dup2_x2, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                     | ((byte) ByteCodeStackBehaviour.PopValue4 << 16)
                                                                                     | ((byte) ByteCodeStackBehaviour.PushValue6 << 8)
                                                                                     | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode Swap = new ByteOpCode(ByteCode.Swap, ((byte) ByteCodeFlowControl.Next << 24)
                                                                               | ((byte) ByteCodeStackBehaviour.PopValue2 << 16)
                                                                               | ((byte) ByteCodeStackBehaviour.PushValue2 << 8)
                                                                               | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode IAdd = new ByteOpCode(ByteCode.IAdd, ((byte) ByteCodeFlowControl.Next << 24)
                                                                               | ((byte) ByteCodeStackBehaviour.PopValue2 << 16)
                                                                               | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                               | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode LAdd = new ByteOpCode(ByteCode.LAdd, ((byte) ByteCodeFlowControl.Next << 24)
                                                                               | ((byte) ByteCodeStackBehaviour.PopValue2 << 16)
                                                                               | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                               | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode FAdd = new ByteOpCode(ByteCode.FAdd, ((byte) ByteCodeFlowControl.Next << 24)
                                                                               | ((byte) ByteCodeStackBehaviour.PopValue2 << 16)
                                                                               | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                               | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode DAdd = new ByteOpCode(ByteCode.DAdd, ((byte) ByteCodeFlowControl.Next << 24)
                                                                               | ((byte) ByteCodeStackBehaviour.PopValue2 << 16)
                                                                               | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                               | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode ISub = new ByteOpCode(ByteCode.ISub, ((byte) ByteCodeFlowControl.Next << 24)
                                                                               | ((byte) ByteCodeStackBehaviour.PopValue2 << 16)
                                                                               | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                               | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode LSub = new ByteOpCode(ByteCode.LSub, ((byte) ByteCodeFlowControl.Next << 24)
                                                                               | ((byte) ByteCodeStackBehaviour.PopValue2 << 16)
                                                                               | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                               | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode FSub = new ByteOpCode(ByteCode.FSub, ((byte) ByteCodeFlowControl.Next << 24)
                                                                               | ((byte) ByteCodeStackBehaviour.PopValue2 << 16)
                                                                               | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                               | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode DSub = new ByteOpCode(ByteCode.DSub, ((byte) ByteCodeFlowControl.Next << 24)
                                                                               | ((byte) ByteCodeStackBehaviour.PopValue2 << 16)
                                                                               | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                               | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode IMul = new ByteOpCode(ByteCode.IMul, ((byte) ByteCodeFlowControl.Next << 24)
                                                                               | ((byte) ByteCodeStackBehaviour.PopValue2 << 16)
                                                                               | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                               | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode LMul = new ByteOpCode(ByteCode.LMul, ((byte) ByteCodeFlowControl.Next << 24)
                                                                               | ((byte) ByteCodeStackBehaviour.PopValue2 << 16)
                                                                               | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                               | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode FMul = new ByteOpCode(ByteCode.FMul, ((byte) ByteCodeFlowControl.Next << 24)
                                                                               | ((byte) ByteCodeStackBehaviour.PopValue2 << 16)
                                                                               | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                               | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode DMul = new ByteOpCode(ByteCode.DMul, ((byte) ByteCodeFlowControl.Next << 24)
                                                                               | ((byte) ByteCodeStackBehaviour.PopValue2 << 16)
                                                                               | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                               | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode IDiv = new ByteOpCode(ByteCode.IDiv, ((byte) ByteCodeFlowControl.Next << 24)
                                                                               | ((byte) ByteCodeStackBehaviour.PopValue2 << 16)
                                                                               | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                               | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode LDiv = new ByteOpCode(ByteCode.LDiv, ((byte) ByteCodeFlowControl.Next << 24)
                                                                               | ((byte) ByteCodeStackBehaviour.PopValue2 << 16)
                                                                               | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                               | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode FDiv = new ByteOpCode(ByteCode.FDiv, ((byte) ByteCodeFlowControl.Next << 24)
                                                                               | ((byte) ByteCodeStackBehaviour.PopValue2 << 16)
                                                                               | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                               | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode DDiv = new ByteOpCode(ByteCode.DDiv, ((byte) ByteCodeFlowControl.Next << 24)
                                                                               | ((byte) ByteCodeStackBehaviour.PopValue2 << 16)
                                                                               | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                               | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode IRem = new ByteOpCode(ByteCode.IRem, ((byte) ByteCodeFlowControl.Next << 24)
                                                                               | ((byte) ByteCodeStackBehaviour.PopValue2 << 16)
                                                                               | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                               | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode LRem = new ByteOpCode(ByteCode.LRem, ((byte) ByteCodeFlowControl.Next << 24)
                                                                               | ((byte) ByteCodeStackBehaviour.PopValue2 << 16)
                                                                               | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                               | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode FRem = new ByteOpCode(ByteCode.FRem, ((byte) ByteCodeFlowControl.Next << 24)
                                                                               | ((byte) ByteCodeStackBehaviour.PopValue2 << 16)
                                                                               | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                               | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode DRem = new ByteOpCode(ByteCode.DRem, ((byte) ByteCodeFlowControl.Next << 24)
                                                                               | ((byte) ByteCodeStackBehaviour.PopValue2 << 16)
                                                                               | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                               | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode INeg = new ByteOpCode(ByteCode.INeg, ((byte) ByteCodeFlowControl.Next << 24)
                                                                               | ((byte) ByteCodeStackBehaviour.PopValue1 << 16)
                                                                               | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                               | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode LNeg = new ByteOpCode(ByteCode.LNeg, ((byte) ByteCodeFlowControl.Next << 24)
                                                                               | ((byte) ByteCodeStackBehaviour.PopValue1 << 16)
                                                                               | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                               | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode FNeg = new ByteOpCode(ByteCode.FNeg, ((byte) ByteCodeFlowControl.Next << 24)
                                                                               | ((byte) ByteCodeStackBehaviour.PopValue1 << 16)
                                                                               | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                               | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode DNeg = new ByteOpCode(ByteCode.DNeg, ((byte) ByteCodeFlowControl.Next << 24)
                                                                               | ((byte) ByteCodeStackBehaviour.PopValue1 << 16)
                                                                               | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                               | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode IShl = new ByteOpCode(ByteCode.IShl, ((byte) ByteCodeFlowControl.Next << 24)
                                                                               | ((byte) ByteCodeStackBehaviour.PopValue2 << 16)
                                                                               | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                               | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode LShl = new ByteOpCode(ByteCode.LShl, ((byte) ByteCodeFlowControl.Next << 24)
                                                                               | ((byte) ByteCodeStackBehaviour.PopValue2 << 16)
                                                                               | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                               | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode IShr = new ByteOpCode(ByteCode.IShr, ((byte) ByteCodeFlowControl.Next << 24)
                                                                               | ((byte) ByteCodeStackBehaviour.PopValue2 << 16)
                                                                               | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                               | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode LShr = new ByteOpCode(ByteCode.LShr, ((byte) ByteCodeFlowControl.Next << 24)
                                                                               | ((byte) ByteCodeStackBehaviour.PopValue2 << 16)
                                                                               | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                               | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode IuShr = new ByteOpCode(ByteCode.IuShr, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                 | ((byte) ByteCodeStackBehaviour.PopValue2 << 16)
                                                                               | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                               | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode LuShr = new ByteOpCode(ByteCode.LuShr, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                 | ((byte) ByteCodeStackBehaviour.PopValue2 << 16)
                                                                               | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                               | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode IAnd = new ByteOpCode(ByteCode.IAnd, ((byte) ByteCodeFlowControl.Next << 24)
                                                                               | ((byte) ByteCodeStackBehaviour.PopValue2 << 16)
                                                                               | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                               | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode LAnd = new ByteOpCode(ByteCode.LAnd, ((byte) ByteCodeFlowControl.Next << 24)
                                                                               | ((byte) ByteCodeStackBehaviour.PopValue2 << 16)
                                                                               | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                               | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode IOr = new ByteOpCode(ByteCode.IOr, ((byte) ByteCodeFlowControl.Next << 24)
                                                                             | ((byte) ByteCodeStackBehaviour.PopValue2 << 16)
                                                                               | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                               | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode LOr = new ByteOpCode(ByteCode.LOr, ((byte) ByteCodeFlowControl.Next << 24)
                                                                             | ((byte) ByteCodeStackBehaviour.PopValue2 << 16)
                                                                               | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                               | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode IXor = new ByteOpCode(ByteCode.IXor, ((byte) ByteCodeFlowControl.Next << 24)
                                                                               | ((byte) ByteCodeStackBehaviour.PopValue2 << 16)
                                                                               | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                               | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode LXor = new ByteOpCode(ByteCode.LXor, ((byte) ByteCodeFlowControl.Next << 24)
                                                                               | ((byte) ByteCodeStackBehaviour.PopValue2 << 16)
                                                                               | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                               | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode IInc = new ByteOpCode(ByteCode.IInc, ((byte) ByteCodeFlowControl.Next << 24)
                                                                               | ((byte) ByteCodeStackBehaviour.None << 16)
                                                                               | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                               | (byte) ByteCodeOperandType.LocalConst);
        public static readonly ByteOpCode I2l = new ByteOpCode(ByteCode.I2l, ((byte) ByteCodeFlowControl.Next << 24)
                                                                             | ((byte) ByteCodeStackBehaviour.PopValue1 << 16)
                                                                             | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                             | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode I2f = new ByteOpCode(ByteCode.I2f, ((byte) ByteCodeFlowControl.Next << 24)
                                                                             | ((byte) ByteCodeStackBehaviour.PopValue1 << 16)
                                                                             | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                             | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode I2d = new ByteOpCode(ByteCode.I2d, ((byte) ByteCodeFlowControl.Next << 24)
                                                                             | ((byte) ByteCodeStackBehaviour.PopValue1 << 16)
                                                                             | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                             | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode L2i = new ByteOpCode(ByteCode.L2i, ((byte) ByteCodeFlowControl.Next << 24)
                                                                             | ((byte) ByteCodeStackBehaviour.PopValue1 << 16)
                                                                             | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                             | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode L2f = new ByteOpCode(ByteCode.L2f, ((byte) ByteCodeFlowControl.Next << 24)
                                                                             | ((byte) ByteCodeStackBehaviour.PopValue1 << 16)
                                                                             | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                             | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode L2d = new ByteOpCode(ByteCode.L2d, ((byte) ByteCodeFlowControl.Next << 24)
                                                                             | ((byte) ByteCodeStackBehaviour.PopValue1 << 16)
                                                                             | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                             | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode F2i = new ByteOpCode(ByteCode.F2i, ((byte) ByteCodeFlowControl.Next << 24)
                                                                             | ((byte) ByteCodeStackBehaviour.PopValue1 << 16)
                                                                             | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                             | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode F2l = new ByteOpCode(ByteCode.F2l, ((byte) ByteCodeFlowControl.Next << 24)
                                                                             | ((byte) ByteCodeStackBehaviour.PopValue1 << 16)
                                                                             | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                             | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode F2d = new ByteOpCode(ByteCode.F2d, ((byte) ByteCodeFlowControl.Next << 24)
                                                                             | ((byte) ByteCodeStackBehaviour.PopValue1 << 16)
                                                                             | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                             | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode D2i = new ByteOpCode(ByteCode.D2i, ((byte) ByteCodeFlowControl.Next << 24)
                                                                             | ((byte) ByteCodeStackBehaviour.PopValue1 << 16)
                                                                             | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                             | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode D2l = new ByteOpCode(ByteCode.D2l, ((byte) ByteCodeFlowControl.Next << 24)
                                                                             | ((byte) ByteCodeStackBehaviour.PopValue1 << 16)
                                                                             | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                             | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode D2f = new ByteOpCode(ByteCode.D2f, ((byte) ByteCodeFlowControl.Next << 24)
                                                                             | ((byte) ByteCodeStackBehaviour.PopValue1 << 16)
                                                                             | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                             | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode I2b = new ByteOpCode(ByteCode.I2b, ((byte) ByteCodeFlowControl.Next << 24)
                                                                             | ((byte) ByteCodeStackBehaviour.PopValue1 << 16)
                                                                             | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                             | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode I2c = new ByteOpCode(ByteCode.I2c, ((byte) ByteCodeFlowControl.Next << 24)
                                                                             | ((byte) ByteCodeStackBehaviour.PopValue1 << 16)
                                                                             | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                             | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode I2s = new ByteOpCode(ByteCode.I2s, ((byte) ByteCodeFlowControl.Next << 24)
                                                                             | ((byte) ByteCodeStackBehaviour.PopValue1 << 16)
                                                                             | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                             | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode ICmp = new ByteOpCode(ByteCode.ICmp, ((byte) ByteCodeFlowControl.Next << 24)
                                                                               | ((byte) ByteCodeStackBehaviour.PopValue2 << 16)
                                                                               | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                               | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode FCmpL = new ByteOpCode(ByteCode.FCmpL, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                 | ((byte) ByteCodeStackBehaviour.PopValue2 << 16)
                                                                               | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                               | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode FCmpG = new ByteOpCode(ByteCode.FCmpG, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                 | ((byte) ByteCodeStackBehaviour.PopValue2 << 16)
                                                                               | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                               | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode DCmpL = new ByteOpCode(ByteCode.DCmpL, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                 | ((byte) ByteCodeStackBehaviour.PopValue2 << 16)
                                                                               | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                               | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode DCmpG = new ByteOpCode(ByteCode.DCmpG, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                 | ((byte) ByteCodeStackBehaviour.PopValue2 << 16)
                                                                               | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                               | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode IfEq = new ByteOpCode(ByteCode.IfEq, ((byte) ByteCodeFlowControl.ConditionalBranch << 24)
                                                                               | ((byte) ByteCodeStackBehaviour.PopValue1 << 16)
                                                                               | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                               | (byte) ByteCodeOperandType.BranchOffset);
        public static readonly ByteOpCode IfNe = new ByteOpCode(ByteCode.IfNe, ((byte) ByteCodeFlowControl.ConditionalBranch << 24)
                                                                               | ((byte) ByteCodeStackBehaviour.PopValue1 << 16)
                                                                               | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                               | (byte) ByteCodeOperandType.BranchOffset);
        public static readonly ByteOpCode IfLt = new ByteOpCode(ByteCode.IfLt, ((byte) ByteCodeFlowControl.ConditionalBranch << 24)
                                                                               | ((byte) ByteCodeStackBehaviour.PopValue1 << 16)
                                                                               | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                               | (byte) ByteCodeOperandType.BranchOffset);
        public static readonly ByteOpCode IfGe = new ByteOpCode(ByteCode.IfGe, ((byte) ByteCodeFlowControl.ConditionalBranch << 24)
                                                                               | ((byte) ByteCodeStackBehaviour.PopValue1 << 16)
                                                                               | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                               | (byte) ByteCodeOperandType.BranchOffset);
        public static readonly ByteOpCode IfGt = new ByteOpCode(ByteCode.IfGt, ((byte) ByteCodeFlowControl.ConditionalBranch << 24)
                                                                               | ((byte) ByteCodeStackBehaviour.PopValue1 << 16)
                                                                               | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                               | (byte) ByteCodeOperandType.BranchOffset);
        public static readonly ByteOpCode IfLe = new ByteOpCode(ByteCode.IfLe, ((byte) ByteCodeFlowControl.ConditionalBranch << 24)
                                                                               | ((byte) ByteCodeStackBehaviour.PopValue1 << 16)
                                                                               | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                               | (byte) ByteCodeOperandType.BranchOffset);
        public static readonly ByteOpCode If_ICmpEq = new ByteOpCode(ByteCode.If_ICmpEq, ((byte) ByteCodeFlowControl.ConditionalBranch << 24)
                                                                                         | ((byte) ByteCodeStackBehaviour.PopValue2 << 16)
                                                                                         | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                                         | (byte) ByteCodeOperandType.BranchOffset);
        public static readonly ByteOpCode If_ICmpNe = new ByteOpCode(ByteCode.If_ICmpNe, ((byte) ByteCodeFlowControl.ConditionalBranch << 24)
                                                                                         | ((byte) ByteCodeStackBehaviour.PopValue2 << 16)
                                                                                         | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                                         | (byte) ByteCodeOperandType.BranchOffset);
        public static readonly ByteOpCode If_ICmpLt = new ByteOpCode(ByteCode.If_ICmpLt, ((byte) ByteCodeFlowControl.ConditionalBranch << 24)
                                                                                         | ((byte) ByteCodeStackBehaviour.PopValue2 << 16)
                                                                                         | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                                         | (byte) ByteCodeOperandType.BranchOffset);
        public static readonly ByteOpCode If_ICmpGe = new ByteOpCode(ByteCode.If_ICmpGe, ((byte) ByteCodeFlowControl.ConditionalBranch << 24)
                                                                                         | ((byte) ByteCodeStackBehaviour.PopValue2 << 16)
                                                                                         | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                                         | (byte) ByteCodeOperandType.BranchOffset);
        public static readonly ByteOpCode If_ICmpGt = new ByteOpCode(ByteCode.If_ICmpGt, ((byte) ByteCodeFlowControl.ConditionalBranch << 24)
                                                                                         | ((byte) ByteCodeStackBehaviour.PopValue2 << 16)
                                                                                         | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                                         | (byte) ByteCodeOperandType.BranchOffset);
        public static readonly ByteOpCode If_ICmpLe = new ByteOpCode(ByteCode.If_ICmpLe, ((byte) ByteCodeFlowControl.ConditionalBranch << 24)
                                                                                         | ((byte) ByteCodeStackBehaviour.PopValue2 << 16)
                                                                                         | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                                         | (byte) ByteCodeOperandType.BranchOffset);
        public static readonly ByteOpCode If_ACmpEq = new ByteOpCode(ByteCode.If_ACmpEq, ((byte) ByteCodeFlowControl.ConditionalBranch << 24)
                                                                                         | ((byte) ByteCodeStackBehaviour.PopValue2 << 16)
                                                                                         | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                                         | (byte) ByteCodeOperandType.BranchOffset);
        public static readonly ByteOpCode If_ACmpNe = new ByteOpCode(ByteCode.If_ACmpNe, ((byte) ByteCodeFlowControl.ConditionalBranch << 24)
                                                                                         | ((byte) ByteCodeStackBehaviour.PopValue2 << 16)
                                                                                         | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                                         | (byte) ByteCodeOperandType.BranchOffset);
        public static readonly ByteOpCode Goto = new ByteOpCode(ByteCode.Goto, ((byte) ByteCodeFlowControl.Branch << 24)
                                                                               | ((byte) ByteCodeStackBehaviour.None << 16)
                                                                               | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                               | (byte) ByteCodeOperandType.BranchOffset);
        public static readonly ByteOpCode Jsr = new ByteOpCode(ByteCode.Jsr, ((byte) ByteCodeFlowControl.Call << 24)
                                                                             | ((byte) ByteCodeStackBehaviour.None << 16)
                                                                             | ((byte) ByteCodeStackBehaviour.PushAddress << 8)
                                                                             | (byte) ByteCodeOperandType.BranchOffset);
        public static readonly ByteOpCode Ret = new ByteOpCode(ByteCode.Ret, ((byte) ByteCodeFlowControl.Return << 24)
                                                                             | ((byte) ByteCodeStackBehaviour.None << 16)
                                                                             | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                             | (byte) ByteCodeOperandType.LocalIndex);
        public static readonly ByteOpCode TableSwitch = new ByteOpCode(ByteCode.TableSwitch, ((byte) ByteCodeFlowControl.ConditionalBranch << 24)
                                                                                             | ((byte) ByteCodeStackBehaviour.PopValue1 << 16)
                                                                                             | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                                             | (byte) ByteCodeOperandType.TableSwitch);
        public static readonly ByteOpCode LookupSwitch = new ByteOpCode(ByteCode.LookupSwitch, ((byte) ByteCodeFlowControl.ConditionalBranch << 24)
                                                                                               | ((byte) ByteCodeStackBehaviour.PopValue1 << 16)
                                                                                               | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                                               | (byte) ByteCodeOperandType.LookupSwitch);
        public static readonly ByteOpCode IReturn = new ByteOpCode(ByteCode.IReturn, ((byte) ByteCodeFlowControl.Return << 24)
                                                                                     | ((byte) ByteCodeStackBehaviour.PopValue1 << 16)
                                                                                     | ((byte) ByteCodeStackBehaviour.PushEmpty << 8)
                                                                                     | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode LReturn = new ByteOpCode(ByteCode.LReturn, ((byte) ByteCodeFlowControl.Return << 24)
                                                                                     | ((byte) ByteCodeStackBehaviour.PopValue1 << 16)
                                                                                     | ((byte) ByteCodeStackBehaviour.PushEmpty << 8)
                                                                                     | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode FReturn = new ByteOpCode(ByteCode.FReturn, ((byte) ByteCodeFlowControl.Return << 24)
                                                                                     | ((byte) ByteCodeStackBehaviour.PopValue1 << 16)
                                                                                     | ((byte) ByteCodeStackBehaviour.PushEmpty << 8)
                                                                                     | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode DReturn = new ByteOpCode(ByteCode.DReturn, ((byte) ByteCodeFlowControl.Return << 24)
                                                                                     | ((byte) ByteCodeStackBehaviour.PopValue1 << 16)
                                                                                     | ((byte) ByteCodeStackBehaviour.PushEmpty << 8)
                                                                                     | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode AReturn = new ByteOpCode(ByteCode.AReturn, ((byte) ByteCodeFlowControl.Return << 24)
                                                                                     | ((byte) ByteCodeStackBehaviour.PopRef << 16)
                                                                                     | ((byte) ByteCodeStackBehaviour.PushEmpty << 8)
                                                                                     | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode Return = new ByteOpCode(ByteCode.Return, ((byte) ByteCodeFlowControl.Return << 24)
                                                                                   | ((byte) ByteCodeStackBehaviour.None << 16)
                                                                                   | ((byte) ByteCodeStackBehaviour.PushEmpty << 8)
                                                                                   | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode GetStatic = new ByteOpCode(ByteCode.GetStatic, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                         | ((byte) ByteCodeStackBehaviour.None << 16)
                                                                                         | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                                         | (byte) ByteCodeOperandType.FieldIndex);
        public static readonly ByteOpCode PutStatic = new ByteOpCode(ByteCode.PutStatic, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                         | ((byte) ByteCodeStackBehaviour.PopValue1 << 16)
                                                                                         | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                                         | (byte) ByteCodeOperandType.FieldIndex);
        public static readonly ByteOpCode GetField = new ByteOpCode(ByteCode.GetField, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                       | ((byte) ByteCodeStackBehaviour.PopRef << 16)
                                                                                       | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                                       | (byte) ByteCodeOperandType.FieldIndex);
        public static readonly ByteOpCode PutField = new ByteOpCode(ByteCode.PutField, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                       | ((byte) ByteCodeStackBehaviour.PopRefPopValue << 16)
                                                                                       | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                                       | (byte) ByteCodeOperandType.FieldIndex);
        public static readonly ByteOpCode InvokeVirtual = new ByteOpCode(ByteCode.InvokeVirtual, ((byte) ByteCodeFlowControl.Call << 24)
                                                                                                 | ((byte) ByteCodeStackBehaviour.VarPop << 16)
                                                                                                 | ((byte) ByteCodeStackBehaviour.VarPush << 8)
                                                                                                 | (byte) ByteCodeOperandType.MethodIndex);
        public static readonly ByteOpCode InvokeSpecial = new ByteOpCode(ByteCode.InvokeSpecial, ((byte) ByteCodeFlowControl.Call << 24)
                                                                                                 | ((byte) ByteCodeStackBehaviour.VarPop << 16)
                                                                                                 | ((byte) ByteCodeStackBehaviour.VarPush << 8)
                                                                                                 | (byte) ByteCodeOperandType.MethodIndex);
        public static readonly ByteOpCode InvokeStatic = new ByteOpCode(ByteCode.InvokeStatic, ((byte) ByteCodeFlowControl.Call << 24)
                                                                                               | ((byte) ByteCodeStackBehaviour.VarPop << 16)
                                                                                               | ((byte) ByteCodeStackBehaviour.VarPush << 8)
                                                                                               | (byte) ByteCodeOperandType.MethodIndex);
        public static readonly ByteOpCode InvokeInterface = new ByteOpCode(ByteCode.InvokeInterface, ((byte) ByteCodeFlowControl.Call << 24)
                                                                                                     | ((byte) ByteCodeStackBehaviour.VarPop << 16)
                                                                                                     | ((byte) ByteCodeStackBehaviour.VarPush << 8)
                                                                                                     | (byte) ByteCodeOperandType.WideIndexCountZero);
        public static readonly ByteOpCode InvokeDynamic = new ByteOpCode(ByteCode.InvokeDynamic, ((byte) ByteCodeFlowControl.Call << 24)
                                                                                                 | ((byte) ByteCodeStackBehaviour.VarPop << 16)
                                                                                                 | ((byte) ByteCodeStackBehaviour.VarPush << 8)
                                                                                                 | (byte) ByteCodeOperandType.DynamicIndex);
        public static readonly ByteOpCode New = new ByteOpCode(ByteCode.New, ((byte) ByteCodeFlowControl.Next << 24)
                                                                             | ((byte) ByteCodeStackBehaviour.None << 16)
                                                                             | ((byte) ByteCodeStackBehaviour.PushRef << 8)
                                                                             | (byte) ByteCodeOperandType.ClassIndex);
        public static readonly ByteOpCode NewArray = new ByteOpCode(ByteCode.NewArray, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                       | ((byte) ByteCodeStackBehaviour.PopValue1 << 16)
                                                                                       | ((byte) ByteCodeStackBehaviour.PushRef << 8)
                                                                                       | (byte) ByteCodeOperandType.PrimitiveType);
        public static readonly ByteOpCode ANewArray = new ByteOpCode(ByteCode.ANewArray, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                         | ((byte) ByteCodeStackBehaviour.PopValue1 << 16)
                                                                                         | ((byte) ByteCodeStackBehaviour.PushRef << 8)
                                                                                         | (byte) ByteCodeOperandType.ClassIndex);
        public static readonly ByteOpCode ArrayLength = new ByteOpCode(ByteCode.ArrayLength, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                             | ((byte) ByteCodeStackBehaviour.PopRef << 16)
                                                                                             | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                                             | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode AThrow = new ByteOpCode(ByteCode.AThrow, ((byte) ByteCodeFlowControl.Throw << 24)
                                                                                   | ((byte) ByteCodeStackBehaviour.PopRef << 16)
                                                                                   | ((byte) ByteCodeStackBehaviour.PushEmptyPushRef << 8)
                                                                                   | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode CheckCast = new ByteOpCode(ByteCode.CheckCast, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                         | ((byte) ByteCodeStackBehaviour.PopRef << 16)
                                                                                         | ((byte) ByteCodeStackBehaviour.PushRef << 8)
                                                                                         | (byte) ByteCodeOperandType.ClassIndex);
        public static readonly ByteOpCode InstanceOf = new ByteOpCode(ByteCode.InstanceOf, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                           | ((byte) ByteCodeStackBehaviour.PopRef << 16)
                                                                                           | ((byte) ByteCodeStackBehaviour.PushValue1 << 8)
                                                                                           | (byte) ByteCodeOperandType.ClassIndex);
        public static readonly ByteOpCode MonitorEnter = new ByteOpCode(ByteCode.MonitorEnter, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                               | ((byte) ByteCodeStackBehaviour.PopRef << 16)
                                                                                               | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                                               | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode MonitorExit = new ByteOpCode(ByteCode.MonitorExit, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                             | ((byte) ByteCodeStackBehaviour.PopRef << 16)
                                                                                             | ((byte) ByteCodeStackBehaviour.VarPush << 8)
                                                                                             | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode Wide = new ByteOpCode(ByteCode.Wide, ((byte) ByteCodeFlowControl.Meta << 24)
                                                                               | ((byte) ByteCodeStackBehaviour.None << 16)
                                                                               | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                               | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode MultiANewArray = new ByteOpCode(ByteCode.MultiANewArray, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                                   | ((byte) ByteCodeStackBehaviour.VarPop << 16)
                                                                                                   | ((byte) ByteCodeStackBehaviour.PushRef << 8)
                                                                                                   | (byte) ByteCodeOperandType.WideIndexByte);
        public static readonly ByteOpCode IfNull = new ByteOpCode(ByteCode.IfNull, ((byte) ByteCodeFlowControl.ConditionalBranch << 24)
                                                                                   | ((byte) ByteCodeStackBehaviour.PopValue1 << 16)
                                                                                   | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                                   | (byte) ByteCodeOperandType.BranchOffset);
        public static readonly ByteOpCode IfNonNull = new ByteOpCode(ByteCode.IfNonNull, ((byte) ByteCodeFlowControl.ConditionalBranch << 24)
                                                                                         | ((byte) ByteCodeStackBehaviour.PopValue1 << 16)
                                                                                         | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                                         | (byte) ByteCodeOperandType.BranchOffset);
        public static readonly ByteOpCode Goto_W = new ByteOpCode(ByteCode.Goto_W, ((byte) ByteCodeFlowControl.Branch << 24)
                                                                                   | ((byte) ByteCodeStackBehaviour.None << 16)
                                                                                   | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                                   | (byte) ByteCodeOperandType.WideBranchOffset);
        public static readonly ByteOpCode Jsr_W = new ByteOpCode(ByteCode.Jsr_W, ((byte) ByteCodeFlowControl.Call << 24)
                                                                                 | ((byte) ByteCodeStackBehaviour.None << 16)
                                                                                 | ((byte) ByteCodeStackBehaviour.PushAddress << 8)
                                                                                 | (byte) ByteCodeOperandType.WideBranchOffset);
        public static readonly ByteOpCode Breakpoint = new ByteOpCode(ByteCode.Breakpoint, ((byte) ByteCodeFlowControl.Break << 24)
                                                                                           | ((byte) ByteCodeStackBehaviour.None << 16)
                                                                                           | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                                           | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode ImpDep1 = new ByteOpCode(ByteCode.ImpDep1, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                     | ((byte) ByteCodeStackBehaviour.None << 16)
                                                                                     | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                                     | (byte) ByteCodeOperandType.None);
        public static readonly ByteOpCode ImpDep2 = new ByteOpCode(ByteCode.ImpDep2, ((byte) ByteCodeFlowControl.Next << 24)
                                                                                     | ((byte) ByteCodeStackBehaviour.None << 16)
                                                                                     | ((byte) ByteCodeStackBehaviour.None << 8)
                                                                                     | (byte) ByteCodeOperandType.None);

    }
}