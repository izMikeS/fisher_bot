namespace FishingForecast.Models
{

    public class FishingResult
    {
        public Criterion1Results Criterion1Result { get; }
        public Criterion2Results Criterion2Result { get; }
        public Criterion3Results Criterion3Result { get; }

        public FishingResult(Criterion1Results criterion1Result,
                             Criterion2Results criterion2Result,
                             Criterion3Results criterion3Result)
        {
            if (criterion1Result == 0
             || criterion2Result == 0
             || criterion3Result == 0)
                throw new System.ArgumentException();

            this.Criterion1Result = criterion1Result;
            this.Criterion2Result = criterion2Result;
            this.Criterion3Result = criterion3Result;
        }
        public override string ToString()
        {
            if (Criterion1Result == 0
             || Criterion2Result == 0
             || Criterion3Result == 0)
                return "";

            var output = new System.Text.StringBuilder(System.String.Empty);
            bool isCrit2Worked = false;
            if (Criterion2Result == Criterion2Results.FivePercentDifference)
            {
                output.Append("Можливо ");
                isCrit2Worked = true;
            }

            switch (Criterion1Result)
            {
                case Criterion1Results.CriticalConditions:
                    output.Append((!isCrit2Worked ? "К" : "к") + "ритичні умови");
                    break;
                case Criterion1Results.HerbivoresThanPredators:
                    output.Append((!isCrit2Worked ? "Л" : "л") + "овитися буде краще травоїдна риба, хижаки гірше");
                    break;
                case Criterion1Results.NormalHerbivoresAndPredators:
                    output.Append((!isCrit2Worked ? "Д" : "д") + "обрі умови, ловитися буде і травоїдна риба, і хижаки");
                    break;
                case Criterion1Results.PredatorsThanHerbivores:
                    output.Append((!isCrit2Worked ? "Л" : "л") + "овитися буде краще хижа риба, травоїдні гірше");
                    break;
            }

            output.Append(", а за температурними показниками ");

            switch (Criterion3Result)
            {
                case Criterion3Results.LowActivity:
                    output.Append("передбачається низька активність.");
                    break;
                case Criterion3Results.OptimalConditions:
                    output.Append("оптимальні умови.");
                    break;
                case Criterion3Results.NotOptimalConditions:
                    output.Append("не зовсім оптимальні умови.");
                    break;
                case Criterion3Results.CriticalConditions:
                    output.Append("критичні умови.");
                    break;
            }

            return output.ToString();
        }

    }



}
