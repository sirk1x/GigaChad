using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shitcord_Statusser
{
    internal class DynamicLinkedList<T>
    {
        T[] data;

        //https://lyric.mackle.im/api?artist=kollegah
        int numIndex = 0;

        int m_NextIndex
        {
            get
            {
                var _np = numIndex;
                numIndex++;
                if (numIndex >= data.Length)
                    numIndex = 0;
                return _np;
            }
        }

        public T m_Next
        { get { return data[m_NextIndex]; }}

        public DynamicLinkedList(T[] theData)
        {
            data = theData;
        }

    }
}
