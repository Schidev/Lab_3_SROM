using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Lab_3
{
    internal class GF
    {
        // poly(x) = x^509 + x^23 + x^3 + x^2 + 1;

        string _basis;
        string _power = "111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111110";
        string _module = "100000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000100000000000000000001101";
        int[] _indexes = new int[] { 0, 2, 3, 23, 509 };

        /*string _power = "10";
        string _module = "111";
        int[] _indexes = new int[] { 0, 1, 2 };*/

        public GF(string basis)
        {
            _basis = basis;
        }

        public string Module
        {
            get { return _module; }
        }

        public string Element
        {
            get { return _basis; }
            set { _basis = value; }
        }

        public void Mod()
        {
            while (this._basis.Length >= this._module.Length)
            {
                for (int q = 0; q < this.Element.Length; q++)
                {
                    if (this.Element[q] != '0')
                    {
                        this.Element = this.Element.Remove(0, q);
                        q = this.Element.Length;
                    }
                    if (q == this.Element.Length - 1)
                    {
                        this.Element = "0";
                        return;
                    }
                }

                if (this._basis.Length >= this._module.Length)
                {
                    GF t = new GF(this._module + String.Concat(Enumerable.Repeat("0", this.Element.Length - this._module.Length)));
                    this.Element = (t + this).Element;

                }
            }
        }

        public string Y()
        {
            GF answer = new GF(this._power);

            return this.Pow(answer);
        }

        public void _Trace()
        {
            string[] powers = new string[] { "1", "10", "100", "1000", "10000", "100000", "1000000", "10000000", "100000000" };
            //string[] powers = new string[] { "01", "10"};

            GF trace = new GF("");
            GF temp = new GF("");
            foreach (var power in powers)
            {
                temp.Element = this.Pow(new GF(power));
                trace = trace + temp;
            }

            Console.WriteLine("Tr({0}) = {1}", this.Element, trace.Element);
        }

        public static BigInteger B(string num)
        {
            BigInteger sum = 0;

            if (num.Length == 1)
            {
                return sum = BigInteger.Parse(num[0].ToString());
            }

            for (int q = 0; q < num.Length; q++)
            {
                var n = BigInteger.Pow(2, num.Length - q - 1);
                sum = sum + BigInteger.Parse(num[q].ToString()) * n;
            }

            return sum;
        }
        public string Pow(GF exponent)
        {
            for (int q = 0; q < exponent.Element.Length; q++)
            {
                if (exponent.Element[q] != '0')
                {
                    exponent.Element = exponent.Element.Remove(0, q);
                    q = exponent.Element.Length;
                }
                if (q == exponent.Element.Length - 1)
                {
                    exponent.Element = "0";
                    q = exponent.Element.Length;
                }
            }

            for (int q = 0; q < this.Element.Length; q++)
            {
                if (this.Element[q] != '0')
                {
                    this.Element = this.Element.Remove(0, q);
                    q = this.Element.Length;
                }
                if (q == this.Element.Length - 1)
                {
                    this.Element = "0";
                    q = this.Element.Length;
                }
            }

            if (exponent._basis == "10")
            {
                return (this * this)._basis;
            }
            else if (exponent._basis == "1")
            {
                return this._basis;
            }
            else
            {
                return ModPow(this, exponent)._basis;
            }
        }

        public GF ModPow(GF baseNum, GF exponent)
        {
            var B = baseNum;


            int i = 0;
            B = B.Gorner(B, exponent.Element, i);
            B.Mod();
            return B;
        }

        public GF Gorner(GF x, string a, int i = 0)
        {
            if (i >= a.Length) return new GF("1");
            return new GF(a[i] + (x * Gorner(x, a, i + 1)).Element);
        }

        public static GF operator +(GF _elem_1, GF _elem_2)
        {
            GF _sum = new GF("");

            if (_elem_1._basis.Length < _elem_2._basis.Length)
            {
                _sum._basis = (_elem_2 + _elem_1)._basis;
            }
            else
            {
                string num_1 = _elem_1._basis;
                string num_2 = _elem_2._basis;

                for (int q = 0; q < num_1.Length - num_2.Length + q; q++)
                {
                    num_2 = "0" + num_2;
                }

                for (int q = 0; q < num_1.Length; q++)
                {
                    if ((num_1[q] == num_2[q]))
                    {
                        _sum.Element = _sum.Element + "0";
                    }
                    else
                    {
                        _sum.Element = _sum.Element + "1";
                    }
                }

                _sum.Mod();
            }

            return _sum;
        }

        public static GF operator *(GF _elem_1, GF _elem_2)
        {
            GF _mult = new GF("");
            string _el_1 = _elem_1.Element;
            string _el_2 = _elem_2.Element;

            for (int q = 0; q < _elem_1.Element.Length; q++)
            {
                if (_elem_1.Element[q] != '0')
                {
                    _elem_1.Element = _elem_1.Element.Remove(0, q);
                    q = _elem_1.Element.Length;
                }
            }

            for (int q = 0; q < _elem_2.Element.Length; q++)
            {
                if (_elem_2.Element[q] != '0')
                {
                    _elem_2.Element = _elem_2.Element.Remove(0, q);
                    q = _elem_2.Element.Length;
                }
            }

            if (B(_elem_1._basis) < B(_elem_2._basis))
            {
                _mult._basis = (_elem_2 * _elem_1)._basis;
            }
            else
            {
                string num_1 = _elem_1._basis;
                string num_2 = _elem_2._basis;

                for (int q = 0; q < _elem_2.Element.Length; q++)
                {
                    if (_elem_2.Element[_elem_2.Element.Length - q - 1] == '1')
                    {
                        _elem_1.Element = _elem_1.Element + String.Concat(Enumerable.Repeat("0", q));
                        _mult.Element = String.Concat(Enumerable.Repeat("0", _elem_1.Element.Length - _mult.Element.Length)) + _mult.Element;
                        _mult.Element = (_mult + _elem_1).Element;
                    }
                    else
                    {
                        _elem_1.Element = String.Concat(Enumerable.Repeat("0", _elem_1.Element.Length + q));
                        _mult.Element = String.Concat(Enumerable.Repeat("0", _elem_1.Element.Length - _mult.Element.Length)) + _mult.Element;
                        _mult.Element = (_mult + _elem_1).Element;
                    }

                    _elem_1.Element = _el_1;
                }


            }

            _mult.Mod();

            return _mult;
        }

    }
}
