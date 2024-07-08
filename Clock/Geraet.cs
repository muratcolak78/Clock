using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clock
{
    class Geraet
    {
        private int _stufe;
        private double _puls;
        private int _minute;
        private readonly double _maxPuls=150;
        private double geschwindigkeit = 0.17;

        public int Stufe { get => _stufe; set => _stufe = value; }
        public double Puls { get => _puls; set => _puls = value; }
        public int Minute { get => _minute; set => _minute = value; }
      
        public double Geschwindigkeit { get => geschwindigkeit; set => geschwindigkeit = value; }

        public double MaxPuls => _maxPuls;

        public void erhoeheLeistungsStufe()
        {
            
            if (Stufe <=2) Stufe++;
            if (Geschwindigkeit<=0.34) Geschwindigkeit*=2;

        }
        public void vermindereleistunsStufe()
        {
            if(Stufe >=2) Stufe--;
            if (Geschwindigkeit >= 0.34) Geschwindigkeit /= 2;
        }
        public double pulsMessen()
        {
            return Puls;
        }
    }
}
