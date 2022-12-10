﻿namespace Crpg.Module.Rating;

/// <summary>
/// Holds an individual's Glicko-2 rating.
/// Glicko-2 ratings are an average skill value, a standard deviation and a volatility (how consistent the player is).
/// Glickman's paper on the algorithm allows scaling of these values to be more directly comparable with existing rating
/// systems such as Elo or USCF's derivation thereof. This implementation outputs ratings at this larger scale.
/// </summary>
internal class CrpgRating
{
    private const float DefaultRating = 1500.0f;
    private const float Multiplier = 173.7178f;

    private float _rating;
    private float _ratingDeviation;

    public CrpgRating(float rating, float deviation, float volatility)
    {
        _rating = rating;
        _ratingDeviation = deviation;
        Volatility = volatility;
    }

    public float Volatility { get; private set; }

    // the following variables are used to hold values temporarily whilst running calculations
    public float WorkingRating { get; set; }
    public float WorkingRatingDeviation { get; set; }
    public float WorkingVolatility { get; set; }

    /// <summary>
    /// Average skill value of the player scaled down to the scale used by the algorithm's internal workings.
    /// </summary>
    public float Glicko2Rating
    {
        get => (_rating - DefaultRating) / Multiplier;
        private set => _rating = value * Multiplier + DefaultRating;
    }

    /// <summary>
    /// Rating deviation of the player scaled down to the scale used by the algorithm's internal workings.
    /// </summary>
    public float Glicko2RatingDeviation
    {
        get => _ratingDeviation / Multiplier;
        private set => _ratingDeviation = value * Multiplier;
    }

    public void FinalizeRating()
    {
        Glicko2Rating = WorkingRating;
        Glicko2RatingDeviation = WorkingRatingDeviation;
        Volatility = WorkingVolatility;

        WorkingRatingDeviation = 0;
        WorkingRating = 0;
        WorkingVolatility = 0;
    }
}
