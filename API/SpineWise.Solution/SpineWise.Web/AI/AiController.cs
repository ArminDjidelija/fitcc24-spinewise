//using Microsoft.ML;
//using Microsoft.ML.Data;
//using Microsoft.ML.Model;
//using Microsoft.ML.Trainers;
//using Microsoft.ML.Transforms;
//using System.IO;
//using System.Runtime.CompilerServices;
//using Microsoft.ML;
//using Microsoft.ML.Data;

//namespace SpineWise.Web.AI
//{
//    public class AiController
//    {


//        public static void AiTrainer()
//        {
//            var mlContext=new MLContext();
//            var data = mlContext.Data.LoadFromTextFile<PostureData>("posturedata.csv", separatorChar: ',');
//            var trainTestSplit = mlContext.Data.TrainTestSplit(data, testFraction: 0.2);
//            var trainData = trainTestSplit.TrainSet;
//            var testData = trainTestSplit.TestSet;

//            var pipeline = mlContext.Transforms.Conversion.MapValueToKey("Label", "Good")
//                .Append(mlContext.Transforms.Concatenate("Features", "UpperBackDistance", "LegDistance", "PressureSensor1", "PressureSensor2", "PressureSensor3"))
//                .Append(mlContext.Transforms.Conversion.MapKeyToValue("Label", ""))
//                .Append(mlContext.Transforms.Conversion.ConvertType("Label", "Good"));

//            var model = pipeline.Fit(trainData);

//            var predictions = model.Transform(testData);
//            var metrics = mlContext.MulticlassClassification.Evaluate(predictions, "Label", "Good");


//           // var predictionEngine=mlContext.Model.CreatePredictionEngine<>()

//        }

//        public static void MlTrainter()
//        {
//            string trainingPath = "posturedata.csv";
//            string validationPath = "validationdata.csv";

//            MLContext context = new MLContext();

//            IDataView trainData = context.Data.LoadFromTextFile<PostureData>(trainingPath, separatorChar: ',',
//                hasHeader: true, allowQuoting: false);

//            IDataView validationData = context.Data.LoadFromTextFile<PostureData>(validationPath, separatorChar: ',',
//                hasHeader: true, allowQuoting: true);


//        }
//    }
//}
