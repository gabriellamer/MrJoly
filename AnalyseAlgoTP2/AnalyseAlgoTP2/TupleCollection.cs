using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyseAlgoTP2
{
    class TupleCollection
    {
        private static TupleCollection _instance;
        private List<List<Int32>> _collection;
        private List<Int32> _promisingTupleCounters;

        public List<List<Int32>> Collection {
            get
            {
                if (_collection == null)
                {
                    _collection = new List<List<Int32>>();
                }

                return _collection;
            }
        }

        public List<Int32> PromisingTupleCounters
        {
            get
            {
                if (_promisingTupleCounters == null)
                {
                    _promisingTupleCounters = new List<Int32>();
                }

                return _promisingTupleCounters;
            }
        }

        private TupleCollection() {}

        public static TupleCollection Instance() {
            if (_instance == null)
            {
                _instance = new TupleCollection();
            }

            return _instance;
        }
    }
}
