using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Betfair.ESAClient.Cache
{
    /// <summary>
    /// A level price size ladder with copy on write snapshot
    /// </summary>
    public class LevelPriceSizeLadder
    {
        /// <summary>
        /// Dictionary of level to LevelPriceSize
        /// </summary>
        private readonly SortedDictionary<int, LevelPriceSize> _levelToPriceSize = new SortedDictionary<int, LevelPriceSize>();
        private IList<LevelPriceSize> _snap = LevelPriceSize.EmptyList;

        public IList<LevelPriceSize> OnPriceChange(bool isImage, List<List<double?>> prices)
        {
            if (isImage)
            {
                //image is replace
                _levelToPriceSize.Clear();
            }

            if (prices != null)
            {
                //changes to apply
                foreach (List<double?> price in prices)
                {
                    LevelPriceSize levelPriceSize = new LevelPriceSize(price);
                    //keep zero's in the ladder as it is fixed depth
                    _levelToPriceSize[levelPriceSize.Level] = levelPriceSize;
                }
            }

            if (isImage || prices != null)
            {
                //update snap on image or if we had cell changes
                _snap = new List<LevelPriceSize>(_levelToPriceSize.Values);
            }
            return _snap;
        }
    }
}
