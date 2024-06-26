﻿// This file was auto-generated by ML.NET Model Builder. 
using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
namespace SpineWise_Web
{
    public partial class MLModelspine
    {
        /// <summary>
        /// model input class for MLModelspine.
        /// </summary>
        #region model input class
        public class ModelInput
        {
            [ColumnName(@"UpperBackDistance")]
            public float UpperBackDistance { get; set; }

            [ColumnName(@"LegDistance")]
            public float LegDistance { get; set; }

            [ColumnName(@"PressureSensor1")]
            public float PressureSensor1 { get; set; }

            [ColumnName(@"PressureSensor2")]
            public float PressureSensor2 { get; set; }

            [ColumnName(@"PressureSensor3")]
            public float PressureSensor3 { get; set; }

            [ColumnName(@"Good")]
            public float Good { get; set; }

        }

        #endregion

        /// <summary>
        /// model output class for MLModelspine.
        /// </summary>
        #region model output class
        public class ModelOutput
        {
            [ColumnName(@"UpperBackDistance")]
            public float UpperBackDistance { get; set; }

            [ColumnName(@"LegDistance")]
            public float LegDistance { get; set; }

            [ColumnName(@"PressureSensor1")]
            public float PressureSensor1 { get; set; }

            [ColumnName(@"PressureSensor2")]
            public float PressureSensor2 { get; set; }

            [ColumnName(@"PressureSensor3")]
            public float PressureSensor3 { get; set; }

            [ColumnName(@"Good")]
            public uint Good { get; set; }

            [ColumnName(@"Features")]
            public float[] Features { get; set; }

            [ColumnName(@"PredictedLabel")]
            public float PredictedLabel { get; set; }

            [ColumnName(@"Score")]
            public float[] Score { get; set; }

        }

        #endregion

        private static string MLNetModelPath = Path.GetFullPath("MLModelspine.zip");

        public static readonly Lazy<PredictionEngine<ModelInput, ModelOutput>> PredictEngine = new Lazy<PredictionEngine<ModelInput, ModelOutput>>(() => CreatePredictEngine(), true);

        /// <summary>
        /// Use this method to predict on <see cref="ModelInput"/>.
        /// </summary>
        /// <param name="input">model input.</param>
        /// <returns><seealso cref=" ModelOutput"/></returns>
        public static ModelOutput Predict(ModelInput input)
        {
            var predEngine = PredictEngine.Value;
            return predEngine.Predict(input);
        }

        private static PredictionEngine<ModelInput, ModelOutput> CreatePredictEngine()
        {
            var mlContext = new MLContext();
            ITransformer mlModel = mlContext.Model.Load(MLNetModelPath, out var _);
            return mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);
        }
    }
}
