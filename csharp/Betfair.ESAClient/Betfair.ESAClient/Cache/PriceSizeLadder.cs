using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Betfair.ESAClient.Cache
{
    /// <summary>
    /// A price size ladder with copy on write snapshot
    /// </summary>
    public class PriceSizeLadder
    {
        /// <summary>
        /// Dictionary of price to PriceSize.
        /// </summary>
        private readonly SortedDictionary<double, PriceSize> _priceToSize;
        private IList<PriceSize> _snap = PriceSize.EmptyList;


        public static PriceSizeLadder NewBack()
        {
            return new PriceSizeLadder(new ReverseComparer<double>(Comparer<double>.Default));
        }

        public static PriceSizeLadder NewLay()
        {
            return new PriceSizeLadder(Comparer<double>.Default);
        }

        private PriceSizeLadder(IComparer<double> comparer)
        {
            _priceToSize = new SortedDictionary<double, PriceSize>(comparer);
        }

        public IList<PriceSize> OnPriceChange(bool isImage, List<List<double?>> prices)
        {
            if (isImage)
            {
                //initial image - so clear cache
                _priceToSize.Clear();
            }
            if (prices != null)
            {
                //changes to apply
                foreach (List<double?> price in prices)
                {
                    PriceSize priceSize = new PriceSize(price);
                    if (priceSize.Size == 0.0)
                    {
                        //zero signifies remove
                        _priceToSize.Remove(priceSize.Price);
                    }
                    else
                    {
                        //update price
                        _priceToSize[priceSize.Price] = priceSize;
                    }
                }
            }
            if (isImage || prices != null)
            {
                //update snap on image or if we had cell changes
                _snap = new List<PriceSize>(_priceToSize.Values);
            }
            return _snap;
        }

    }
}
