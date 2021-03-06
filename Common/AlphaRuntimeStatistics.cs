﻿using System;
using System.Collections.Generic;
using QuantConnect.Algorithm.Framework.Alphas;
using QuantConnect.Securities;

namespace QuantConnect
{
    /// <summary>
    /// Contains alpha population run time statistics
    /// </summary>
    public class AlphaRuntimeStatistics
    {
        /// <summary>
        /// Gets the mean scores for the entire population of alphas
        /// </summary>
        public AlphaScore MeanPopulationScore { get; } = new AlphaScore();

        /// <summary>
        /// Gets the 100 alpha ema of alpha scores
        /// </summary>
        public AlphaScore RollingAveragedPopulationScore { get; } = new AlphaScore();

        /// <summary>
        /// Gets the total number of alphas with an up direction
        /// </summary>
        public long LongCount { get; set; }

        /// <summary>
        /// Gets the total number of alphas with a down direction
        /// </summary>
        public long ShortCount { get; set; }

        /// <summary>
        /// The ratio of <see cref="AlphaDirection.Up"/> over <see cref="AlphaDirection.Down"/>
        /// </summary>
        public decimal LongShortRatio => ShortCount == 0 ? 1m : LongCount / (decimal) ShortCount;

        /// <summary>
        /// The total estimated value of trading all alphas
        /// </summary>
        public decimal TotalEstimatedAlphaValue { get; set; }

        /// <summary>
        /// The total number of alpha signals generated by the algorithm
        /// </summary>
        public long TotalAlphasGenerated { get; set; }

        /// <summary>
        /// The total number of alpha signals generated by the algorithm
        /// </summary>
        public long TotalAlphasClosed { get; set; }

        /// <summary>
        /// The total number of alpha signals generated by the algorithm
        /// </summary>
        public long TotalAlphasAnalysisCompleted { get; set; }

        /// <summary>
        /// Gets the mean estimated alpha value
        /// </summary>
        public decimal MeanPopulationEstimatedAlphaValue => TotalAlphasClosed > 0 ? TotalEstimatedAlphaValue / TotalAlphasClosed : 0;

        /// <summary>
        /// Creates a dictionary containing the statistics
        /// </summary>
        public Dictionary<string, string> ToDictionary()
        {
            var accountCurrencySymbol = Currencies.GetCurrencySymbol(CashBook.AccountCurrency);
            return new Dictionary<string, string>
            {
                {"Total Alphas Generated", $"{TotalAlphasGenerated}"},
                {"Total Alphas Closed", $"{TotalAlphasClosed}"},
                {"Total Alphas Analysis Completed", $"{TotalAlphasAnalysisCompleted}"},
                {"Long Alpha Count", $"{LongCount}"},
                {"Short Alpha Count", $"{ShortCount}"},
                {"Long/Short Ratio", $"{Math.Round(100*LongShortRatio, 2)}%"},
                {"Total Estimated Alpha Value", $"{accountCurrencySymbol}{TotalEstimatedAlphaValue.SmartRounding()}"},
                {"Mean Population Estimated Alpha Value", $"{accountCurrencySymbol}{MeanPopulationEstimatedAlphaValue.SmartRounding()}"},
                {"Mean Population Direction", $"{Math.Round(100 * MeanPopulationScore.Direction, 4)}%"},
                {"Mean Population Magnitude", $"{Math.Round(100 * MeanPopulationScore.Magnitude, 4)}%"},
                {"Rolling Averaged Population Direction", $"{Math.Round(100 * RollingAveragedPopulationScore.Direction, 4)}%"},
                {"Rolling Averaged Population Magnitude", $"{Math.Round(100 * RollingAveragedPopulationScore.Magnitude, 4)}%"},
            };
        }
    }
}