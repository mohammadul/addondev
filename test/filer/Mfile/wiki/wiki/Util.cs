﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wiki {
    class Util {
        public class Tuple<T1, T2> {
            public T1 t1;
            public T2 t2;

            public Tuple() { }

            public Tuple(T1 t1, T2 t2) {
                this.t1 = t1;
                this.t2 = t2;
            }
        }

        public class Tuple<T1, T2, T3> {
            public T1 t1;
            public T2 t2;
            public T3 t3;

            public Tuple() { }

            public Tuple(T1 t1, T2 t2, T3 t3) {
                this.t1 = t1;
                this.t2 = t2;
                this.t3 = t3;
            }
        }
    }
}
