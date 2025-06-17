using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KUAPPAS
{

    public class M3encryption
    {
        public static string Key = "";
        public static Boolean Randomization = false;
        public static Boolean Fastmode = false;
        public string ClearTekst;
        public string EncryptedTekst;
        public int errorState = 0;

        private const int conMAXch = 198;
        private int[] mapA = new int[conMAXch];
        private int[] mapARev = new int[conMAXch];
        private int[] mapB = new int[conMAXch];
        private int[] mapBRev = new int[conMAXch];
        private int[] mapK = new int[conMAXch];
        private int[] mapKRev = new int[conMAXch];
        private long seedC = 0;
        private long seedK = 0;
        private int keyIndexA = 0;
        private int keyIndexB = 0;
        private int keyIndexK = 0;
        private string mutKeyA_o; //outer
        private string mutKeyB_o; //outer
        private string mutKeyK_o; //outer
        private string mutKeyA_i; //inner
        private string mutKeyB_i; //inner

        private int changeByte(int byteChar, int keyLen, long tekstLen, string mode)
        {
            int intRef;
            int changedChar;
            long seed;

            intRef = ((int)byteChar);
            if (mode == "C")
            {
                genSeedC(keyLen);
                seed = seedC;
            }
            else
            {
                genSeedK();
                seed = seedK;
            }
            changedChar = (byte)(changeChar(intRef, seed, mode));

            return changedChar;
        }

        private int changeByteRev(int byteChar, int keyLen, long tekstLen, string mode)
        {
            int intRef;
            int changedChar;
            long seed;

            intRef = ((int)byteChar);
            if (mode == "C")
            {
                genSeedC(keyLen);
                seed = seedC;
            }
            else
            {
                genSeedK();
                seed = seedK;
            }
            changedChar = (byte)(changeCharRev(intRef, seed, mode));

            return changedChar;
        }

        private int createRefIndex(long seed)
        {
            long refIndex = 1;
            double floatValue;
            double floatMAXch = conMAXch;
            int intPart;
            double fraPart;

            refIndex = seed;

            if (refIndex > conMAXch)
            {
                if (refIndex - conMAXch <= conMAXch)
                    refIndex = refIndex - conMAXch;
                else
                {
                    floatValue = refIndex / floatMAXch;
                    intPart = (int)floatValue;
                    fraPart = floatValue - intPart;
                    if (fraPart == 0.0) fraPart = 1.0;
                    refIndex = Convert.ToInt32(conMAXch * fraPart);
                }
            }

            return (int)refIndex;
        }

        private int changeChar(int tegn, long seed, string mode)
        {
            int refIndex;
            int tegnChanged = tegn;

            try
            {
                refIndex = createRefIndex(seed);
                tegnChanged = tegn + (refIndex - 1);
                if (tegnChanged > conMAXch) tegnChanged = tegnChanged - conMAXch;

                if (mode == "C")
                {
                    tegnChanged = mapA[mapB[mapK[tegnChanged - 1] - 1] - 1];
                }
                else
                {
                    tegnChanged = mapK[tegnChanged - 1];
                }
            }
            catch (Exception )
            {

                errorState = -3;
            }

            return tegnChanged;
        }

        private int changeCharRev(int tegn, long seed, string mode)
        {
            int refIndex;
            int tegnChanged = tegn;

            try
            {
                if (mode == "C")
                {
                    tegn = mapKRev[mapBRev[mapARev[tegn - 1] - 1] - 1];
                }
                else
                {
                    tegn = mapKRev[tegn - 1];
                }

                refIndex = createRefIndex(seed);
                tegnChanged = tegn - (refIndex - 1);
                if (tegnChanged < 0) tegnChanged = tegnChanged * (-1);
                if (tegn < refIndex) tegnChanged = conMAXch - tegnChanged;
            }
            catch (Exception )
            {

                errorState = -3;
            }

            return tegnChanged;
        }

        private string chainChangeChars(string tekst, string mode)
        {
            char tegn1;
            char tegn2;
            char tegn3;

            for (int i = 0; i < tekst.Length - 1; i++)
            {
                tegn1 = Convert.ToChar(tekst[i]);
                tegn2 = Convert.ToChar(tekst[i + 1]);

                tegn3 = (char)linkTegn(tegn1, tegn2, mode);
                tekst = tekst.Substring(0, i + 1) + tegn3 + tekst.Substring(i + 2, tekst.Length - (i + 2));
            }

            for (int i = tekst.Length - 2; i > -1; i--)
            {
                tegn1 = Convert.ToChar(tekst[i + 1]);
                tegn2 = Convert.ToChar(tekst[i]);
                tegn3 = (char)linkTegn(tegn1, tegn2, mode);
                tekst = tekst.Substring(0, i) + tegn3 + tekst.Substring(i + 1, tekst.Length - (i + 1));
            }

            return tekst;
        }

        private string chainChangeCharsRev(string tekst, string mode)
        {
            char tegn1;
            char tegn2;
            char tegn3;

            for (int i = 0; i < tekst.Length - 1; i++)
            {
                tegn1 = Convert.ToChar(tekst[i + 1]);
                tegn2 = Convert.ToChar(tekst[i]);
                tegn3 = (char)linkTegnRev(tegn1, tegn2, mode);
                tekst = tekst.Substring(0, i) + tegn3 + tekst.Substring(i + 1, tekst.Length - (i + 1));
            }

            for (int i = tekst.Length - 2; i > -1; i--)
            {
                tegn1 = Convert.ToChar(tekst[i]);
                tegn2 = Convert.ToChar(tekst[i + 1]);
                tegn3 = (char)linkTegnRev(tegn1, tegn2, mode);
                tekst = tekst.Substring(0, i + 1) + tegn3 + tekst.Substring(i + 2, tekst.Length - (i + 2));
            }

            return tekst;
        }

        private char linkTegn(int tegn1, int tegn2, string mode)
        {
            int tegnRes;

            tegn1 = charDecToIndex(tegn1);
            tegn2 = charDecToIndex(tegn2);

            tegnRes = tegn1 + tegn2;
            if (tegnRes > conMAXch) tegnRes = tegnRes - conMAXch;
            if (mode == "C")
            {
                return (char)indexToCharDec(mapA[mapB[mapK[tegnRes - 1] - 1] - 1]);
            }
            else
            {
                return (char)indexToCharDec(mapK[tegnRes - 1]);
            }
        }

        private char linkTegnRev(int tegn1, int tegn2, string mode)
        {
            int tegnRes;

            tegn1 = charDecToIndex(tegn1);
            tegn2 = charDecToIndex(tegn2);

            if (mode == "C")
            {
                tegn2 = mapKRev[mapBRev[mapARev[tegn2 - 1] - 1] - 1];
            }
            else
            {
                tegn2 = mapKRev[tegn2 - 1];
            }

            tegnRes = tegn2 - tegn1;
            if (tegnRes < 1) tegnRes = tegnRes + conMAXch;

            return (char)indexToCharDec(tegnRes);
        }

        private void shuffleArr(int state, ref int[] refMap, ref int[] refMapRev, int minRounds, ref string key, Boolean selfMutKey)
        {
            int keyStep = 0;
            byte keyChar;
            bool emptyFound;
            int retning = 1;
            long refIndex = 1;
            double floatValue;
            double floatMAXch = conMAXch;
            int intPart;
            double fraPart;

            if (selfMutKey) key = selfMutateKey(key, minRounds, state, 1);
            for (int i = 0; i < conMAXch; i++) refMap[i] = 0; //nulstil arr
            for (int i = 1; i < conMAXch + 1; i++)
            {
                if (retning == 1) retning = 0;
                else retning = 1;

                //handle key
                if (keyStep > key.Length - 1) keyStep = 0;

                keyChar = (byte)key[keyStep];
                refIndex = i + (int)keyChar;

                if (refIndex > conMAXch)
                {
                    if (refIndex - conMAXch <= conMAXch) refIndex = refIndex - conMAXch;
                    else
                    {
                        floatValue = refIndex / floatMAXch;
                        intPart = (int)floatValue;
                        fraPart = floatValue - intPart;
                        if (fraPart == 0.0) fraPart = 1.0;
                        refIndex = Convert.ToInt32(conMAXch * fraPart);
                    }
                }

                refIndex--; //pga arr 0- conMAXch - 1
                if (refMap[refIndex] == 0) refMap[refIndex] = i;
                else
                {
                    emptyFound = false;
                    do
                    {
                        if (retning == 1)
                        {
                            refIndex++;
                            if (refIndex > conMAXch - 1) refIndex = 0;
                            if (refMap[refIndex] == 0)
                            {
                                refMap[refIndex] = i;
                                emptyFound = true;
                            }
                        }
                        else
                        {
                            refIndex--;
                            if (refIndex < 0) refIndex = conMAXch - 1;
                            if (refMap[refIndex] == 0)
                            {
                                refMap[refIndex] = i;
                                emptyFound = true;
                            }
                        }

                    } while (emptyFound == false);
                }

                keyStep++;
            }

            for (int i = 0; i < conMAXch; i++)
            {
                for (int k = 0; k < conMAXch; k++)
                {
                    if (refMap[k] == i + 1)
                    {
                        refMapRev[i] = (k + 1);
                        break;
                    }
                }
            }
        }

        private void genSeedC(int keyLen)
        {
            int keyLenN = 1;
            seedC = seedC + (byte)Convert.ToChar(mutKeyA_i[keyIndexA]);
            seedC = seedC + (byte)Convert.ToChar(mutKeyB_i[keyIndexA]);
            keyIndexA++;
            keyIndexB++;
            if (keyIndexA > keyLen - 1)
            {
                keyIndexA = 0;
                mutKeyA_i = selfMutateKey(mutKeyA_i, 2, 1, 2);
            }
            if (keyLen > 2) keyLenN = keyLen - 1;
            if (keyLen >= 4) keyLenN = keyLen - 3;
            if (keyIndexB > keyLenN - 1)
            {
                keyIndexB = 0;
                mutKeyB_i = selfMutateKey(mutKeyB_i, 2, 1, 2);
            }
        }

        private void genSeedK()
        {
            int keyLen = Key.Length;

            seedK = seedK + (byte)Convert.ToChar(mutKeyK_o[keyIndexK]);
            keyIndexK++;
            if (keyIndexK > keyLen - 1) keyIndexK = 0;
        }

        private string encrypt_decrypt(string tekst, int state, int keyLen, long tekstLen, string mode)
        {
            int changedByte;
            int charIndex;
            string encryptTekst = "";

            if (state == 1) tekst = chainChangeChars(tekst, mode);
            foreach (char ch in tekst)
            {
                charIndex = charDecToIndex((int)ch); //rtf-16 to index
                if (state == 0)
                    changedByte = changeByteRev(charIndex, keyLen, tekstLen, mode);
                else
                    changedByte = changeByte(charIndex, keyLen, tekstLen, mode);

                changedByte = indexToCharDec(changedByte);//index back to rtf-16
                encryptTekst = encryptTekst + Convert.ToString(Convert.ToChar(changedByte));
            }
            if (state == 0) encryptTekst = chainChangeCharsRev(encryptTekst, mode);

            return encryptTekst;
        }

        private void startProcess(int state)
        {
            string encryptTekst;
            string normalTekst;
            long tekstLen = 0;
            int keyLen = 0;
            long rounds = 2;

            bootStrapSelfMut();
            if (state == 1)
            {
                normalTekst = ClearTekst;
                prepRandomization(ref normalTekst);
            }
            else
                normalTekst = EncryptedTekst;

            preSeed(ref tekstLen, ref normalTekst, ref keyLen, ref rounds);
            prepMutKey();
            if (state == 0) shuffleKeyToEndState("");

            encryptTekst = normalTekst;
            for (int r = 1; r <= rounds; r++) //Rounds
            {
                shuffleArr(state, ref mapA, ref mapARev, 3, ref mutKeyA_o, true);
                shuffleArr(state, ref mapB, ref mapBRev, 3, ref mutKeyB_o, true);

                mutKeyA_i = mutKeyA_o; //Initiate inner key by outer key in each round
                mutKeyB_i = mutKeyB_o;
                keyIndexA = 0;
                keyIndexB = 0;
                seedC = (int)Key.Length + conMAXch + 1;//Initiate seedC
                encryptTekst = encrypt_decrypt(encryptTekst, state, keyLen, tekstLen, "C");
            }

            if (state == 0)
            {
                prepRandomizationRev(ref encryptTekst);
                ClearTekst = encryptTekst;
            }
            else
                EncryptedTekst = encryptTekst;
        }

        private void prepMutKey()
        {
            mutKeyA_o = Key + Key + Key + Key + Key;
            mutKeyB_o = revStr(Key + Key + Key + Key + Key);
        }

        private string revStr(string str)
        {
            char[] charArray = str.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        private void shuffleKeyToEndState(string normalTekst)
        {
            long tekstLen = 0;
            int keyLen = 0;
            long rounds = 2;

            preSeed(ref tekstLen, ref normalTekst, ref keyLen, ref rounds);

            for (int r = 1; r <= rounds + 1; r++) //Rounds
            {
                mutKeyA_o = selfMutateKey(mutKeyA_o, 3, 1, 1);
                mutKeyB_o = selfMutateKey(mutKeyB_o, 3, 1, 1);
            }
        }

        private string selfMutateKey(string key, long minRounds, int state, int Kmode)
        {
            string encryptKey;
            string clearKey;
            long tekstLen = 0;
            int keyLen = 0;
            long rounds;

            clearKey = key;
            tekstLen = clearKey.Length;

            if (Kmode == 1)
                mutKeyK_o = Key;
            else
                mutKeyK_o = mutKeyA_o;

            rounds = minRounds + nMax(Key.Length, 2);

            encryptKey = clearKey;
            for (int r = 1; r <= rounds; r++)//rounds
            {
                keyIndexK = 0;
                seedK = (int)tekstLen + conMAXch + 1;
                encryptKey = encrypt_decrypt(encryptKey, state, keyLen, tekstLen, "K");
            }

            return encryptKey;
        }

        private void preSeed(ref long tekstLen, ref string normalTekst, ref int keyLen, ref long rounds)
        {
            tekstLen = normalTekst.Length;

            rounds = 8 + nMax(Key.Length, 5);
            if (Fastmode) rounds = 1 + nMax(Key.Length, 2);

            if ((Key.Length * 5) >= tekstLen)
                keyLen = (int)tekstLen / 2;
            else
                keyLen = Key.Length * 5;
        }

        private int nMax(int n, double max)
        {
            double floatValue;
            int intPart;
            double fraPart;

            if (n > max)
            {
                floatValue = n / max;
                intPart = (int)floatValue;
                fraPart = floatValue - intPart;
                if (fraPart == 0.0) fraPart = 1.0;
                n = Convert.ToInt32(max * fraPart);
            }
            return n;
        }

        private void prepRandomization(ref string normalTekst)
        {
            int ranN;
            char ranChar;

            if (Randomization)
            {
                Random rnd = new Random();
                ranN = rnd.Next(32, 127);
                ranChar = (char)((byte)ranN);
                normalTekst = ranChar + normalTekst;
            }
            else
                normalTekst = normalTekst[0] + normalTekst;
        }

        private int charDecToIndex(int ch)
        {
            int index = 1;
            //Index = [1 - 198] set conMAXch = 198
            //Index
            if (ch == 10) index = ch - 9;                           //1
            else if (ch == 13) index = ch - 11;                     //2
            else if ((ch >= 32) && (ch <= 126)) index = ch - 29;    //3-97
            else if ((ch >= 161) && (ch <= 172)) index = ch - 63;   //98-109
            else if ((ch >= 174) && (ch <= 255)) index = ch - 64;   //110-191
            else if (ch == 8211) index = ch - 8019;                 //192
            else if (ch == 8212) index = ch - 8019;                 //193
            else if (ch == 8217) index = ch - 8023;                 //194
            else if (ch == 8220) index = ch - 8025;                 //195
            else if (ch == 8221) index = ch - 8025;                 //196
            else if (ch == 8230) index = ch - 8033;                 //197
            else if (ch == 8364) index = ch - 8166;                 //198
            else
            {
                errorState = -1;
                index = 34;//?
            }

            return index;
        }

        private int indexToCharDec(int index)
        {
            int ch = 1;
            //utf-16
            if (index == 1) ch = index + 9;                             //10
            else if (index == 2) ch = index + 11;                       //13
            else if ((index >= 3) && (index <= 97)) ch = index + 29;    //32-126
            else if ((index >= 98) && (index <= 109)) ch = index + 63;  //161-172
            else if ((index >= 110) && (index <= 191)) ch = index + 64; //174-255
            else if (index == 192) ch = index + 8019;                   //8211
            else if (index == 193) ch = index + 8019;                   //8212
            else if (index == 194) ch = index + 8023;                   //8217
            else if (index == 195) ch = index + 8025;                   //8220
            else if (index == 196) ch = index + 8025;                   //8221
            else if (index == 197) ch = index + 8033;                   //8230
            else if (index == 198) ch = index + 8166;                   //8364

            return ch;
        }

        private void prepRandomizationRev(ref string normalTekst)
        {
            normalTekst = normalTekst.Substring(1, normalTekst.Length - 1);
        }

        private void bootStrapSelfMut()
        {
            shuffleArr(1, ref mapK, ref mapKRev, 0, ref Key, false);
            shuffleArr(1, ref mapK, ref mapKRev, 10, ref Key, true);
            shuffleArr(1, ref mapK, ref mapKRev, 20, ref Key, true);
        }

        public void Encrypt()
        {
            if (Key.Length > 0)
            {
                if (ClearTekst.Length > 0) startProcess(1);
            }
            else
                errorState = -2;
        }

        public void Decrypt()
        {
            if (Key.Length > 0)
            {
                if (EncryptedTekst.Length > 0) startProcess(0);
            }
            else
                errorState = -2;
        }
    }
}
