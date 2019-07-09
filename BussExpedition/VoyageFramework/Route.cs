using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoyageFramework
{
    public class Route
    {
        private int _breakcount;
        private const int _breakCountPerDistance = 200;
        private const int _pricestep = 25;
        private const int _underkm300price = 5;
        private const double _overkm300price = 4.25;
        private const int _priceoverlevel = 300;
        private const int _durationstepseconds = 45;
        private const int _breakmininute = 30;
        private double _totalDuration;


        public string Name
        {
            get
            {
                if (BreakCount == 0)
                {
                    return String.Format("{0} - {1} / {2} KM'lik Express Rota", DepartureLocation, ArrivalLocation, Distance);
                }
                return String.Format("{0} - {1} / {2} KM'lik {3} Molalı Rota", DepartureLocation, ArrivalLocation, Distance, BreakCount);
            }
        }
        public string DepartureLocation { get; }
        public string ArrivalLocation { get; }
        public int BreakCount
        {
            get { return _breakcount; }
            set
            {
                if (value > Distance / _breakCountPerDistance)
                {
                    _breakcount = Distance / _breakCountPerDistance;
                }

                else if (value < 0)
                {
                    _breakcount = 0;
                }
                else
                {
                    _breakcount = value;
                }

            }
        }
        public int Distance { get; }
        public int Duration
        {
            get
            {
                _totalDuration = (Distance * _durationstepseconds + BreakCount * _breakmininute * 60) / 60.0;
                if (_totalDuration % 1 != 0)
                {
                    _totalDuration++;
                }

                return (int)_totalDuration;
            }
        }
        public decimal Baseprice
        {
            get
            {
                if (Distance < _priceoverlevel)
                {
                    return (decimal)(Math.Ceiling((decimal)Distance / _pricestep)) * _underkm300price;
                }
                else
                {
                    return (decimal)((Math.Ceiling((double)(Distance - _priceoverlevel) / _pricestep)) * _overkm300price)
                        +_underkm300price*12;
                }
            }

            //{
            //    if (Distance < _priceoverlevel)
            //    {
            //        if (Distance % _pricestep != 0)
            //        {
            //            return ((Distance / _pricestep) + 1) * _underkm300price;
            //        }
            //        return (Distance / _pricestep) * _underkm300price;
            //    }
            //    if (Distance % _pricestep != 0)
            //    {
            //        return ((((Distance - _priceoverlevel) / _pricestep) + 1) * _overkm300price) + 60;
            //    }
            //    return (((Distance - _priceoverlevel) / _pricestep) * _overkm300price) + 60;
            //}
        }
        public Route(string departurelocation, string arrivallocation, int distance)
        {
            DepartureLocation = departurelocation;
            ArrivalLocation = arrivallocation;
            Distance = distance;

        }

    }
}
