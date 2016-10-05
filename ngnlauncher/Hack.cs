using System;

namespace ngnlauncher
{
    public class Hack
    {
        private UIntPtr _address;
        private byte[] _opcodes;
        private int _size;
        public Hack(UIntPtr address, params byte[] opcodes)
        {
            this.SetAddress(address);
            this.SetOpcodes(opcodes);
        }
        public Hack(int address, params byte[] opcodes)
        {
            this.SetAddress((UIntPtr)address);
            this.SetOpcodes(opcodes);
        }
        public void SetAddress(UIntPtr address)
        {
            this._address = address;
        }
        public void SetOpcodes(byte[] opcodes)
        {
            this._opcodes = opcodes;
            this._size = opcodes.Length;
        }
        public UIntPtr GetAddress()
        {
            return this._address;
        }
        public byte[] GetOpcodes()
        {
            return this._opcodes;
        }
        public int GetSize()
        {
            return this._size;
        }
    }
}
