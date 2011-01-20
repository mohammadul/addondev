﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace mftread {
    class Program {
        static void Main(string[] args) {
            //CallBackTenTimes(
            //    new CallBackTenTimesProc(MyCallBackTenTimesProc)
            //);
            IntPtr pListB = IntPtr.Zero;
            

            // リストデータ取得
            customList(out pListB);

            // ポインタを構造体へ変換
            RECORD listB = (RECORD)Marshal.PtrToStructure(pListB, typeof(RECORD));

            IntPtr pListA = IntPtr.Zero;
            //IntPtr pListA = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(RECORD))*2);

            RECORD[] aryA = new RECORD[2];
            customLists(out pListA);
            int sizes = Marshal.SizeOf(typeof(RECORD));
            for (int i = 0; i < 2; i++) {
                //ポインタを、sizeずつずらしていく。
                //IntPtr current = new IntPtr(pListA.ToInt64() + (sizes * i));
                IntPtr current = (IntPtr)((int)pListA + (sizes * i));
                //ポインタから構造体に変換して配列に格納。
                aryA[i] = (RECORD)Marshal.PtrToStructure(current, typeof(RECORD));

                //lstPointer = (IntPtr)((int)lstPointer + Marshal.SizeOf(lstArray[i]));
            }

            freeBuffer(pListA);

            foreach (var item in aryA) {
                Console.WriteLine(item.index);
                Console.WriteLine(item.name);
            }

            RECORD sysTime = new RECORD();
            //IntPtr sysTimePtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(sysTime));

            //GetRecordS(ref sysTimePtr);
            //sysTime = (RECORD)Marshal.PtrToStructure(sysTimePtr, sysTime.GetType());


            IntPtr aryXPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(RECORD)) * 10); //IntPtr.Zero;
            GetRecord(ref aryXPtr);
            RECORD[] aryX = new RECORD[10];

            int size = Marshal.SizeOf(typeof(RECORD));
            for (int i = 0; i <10; i++) {
                //ポインタを、sizeずつずらしていく。
                //IntPtr current = new IntPtr(aryXPtr.ToInt64() + (size * i));
                IntPtr current = (IntPtr)((int)aryXPtr + (size * i));
                //ポインタから構造体に変換して配列に格納。
                aryX[i] = (RECORD)Marshal.PtrToStructure(current, typeof(RECORD));

                //unsafe {
                //    RECORD obj = *(RECORD*)current;
                //    int k = 0;
                //}

                //lstPointer = (IntPtr)((int)lstPointer + Marshal.SizeOf(lstArray[i]));

            }

            //Marshal.FreeCoTaskMem(aryXPtr);

            foreach (var item in aryX) {
                Console.WriteLine(item.index);
            }
        }

        static void MyCallBackTenTimesProc() {
            Console.WriteLine("Hello, Callback!");
        }

        delegate void CallBackTenTimesProc();

        [DllImport("MFTReader.dll")]
        static extern unsafe void CallBackTenTimes(CallBackTenTimesProc proc);


        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct RECORD {
            public Int32 index;
            public Int32 ChangeTime;

            [MarshalAsAttribute(UnmanagedType.LPWStr)]
            public String name;
        }

        [DllImport("MFTReader.dll")]
        static extern unsafe void GetRecord(ref IntPtr proc);

        [DllImport("MFTReader.dll")]
        static extern unsafe void GetRecordS(ref IntPtr proc);

        [DllImport("MFTReader.dll")]
        static extern unsafe void customList(out IntPtr p);


        [DllImport("MFTReader.dll")]
        static extern unsafe void customLists(out IntPtr p);


        [DllImport("MFTReader.dll")]
        static extern unsafe void freeBuffer(IntPtr p);
    }

}