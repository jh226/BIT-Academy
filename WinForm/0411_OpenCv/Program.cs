using OpenCvSharp;
using System;
using System.Collections.Generic;
using ZXing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OpenCvSharp.ConnectedComponents;


namespace _0411_OpenCv
{
    internal class Program
    {
        #region 이미지 전처리
        static void Exam1() //색 변환
        {
            Mat src = Cv2.ImRead("sample.png");
            Mat dst = new Mat(src.Size(), MatType.CV_8UC1);

            Cv2.CvtColor(src, dst, ColorConversionCodes.BGR2GRAY);

            Cv2.ImShow("dst", dst);
            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();
        }
        static void Exam2() //대칭 
        {
            Mat src = Cv2.ImRead("sample.png");
            Mat dst = new Mat(src.Size(), MatType.CV_8UC3);

            Cv2.Flip(src, dst, FlipMode.Y);

            Cv2.ImShow("dst", dst);
            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();
        }
        static void Exam3() //확대/축소
        {
            Mat src = new Mat("sample.png", ImreadModes.ReducedColor2);
            Mat pyrUp = new Mat();
            Mat pyrDown = new Mat();

            Cv2.PyrUp(src, pyrUp);
            Cv2.PyrDown(src, pyrDown);

            Cv2.ImShow("pyrUp", pyrUp);
            Cv2.ImShow("pyrDown", pyrDown);
            Cv2.WaitKey(0);
        }
        static void Exam4() //크기조절
        {
            Mat src = new Mat("sample.png", ImreadModes.ReducedColor2);
            Mat dst = new Mat();

            Cv2.Resize(src, dst, new Size(500, 250));

            Cv2.ImShow("src", src);
            Cv2.ImShow("dst", dst);
            Cv2.WaitKey(0);
        }
        static void Exam5() //자르기
        {
            Mat src = new Mat("sample.png", ImreadModes.ReducedColor2);
            Mat dst = src.SubMat(new Rect(100, 100, 100, 100));

            Cv2.ImShow("src", src);
            Cv2.ImShow("dst", dst);
            Cv2.WaitKey(0);
        }
        static void Exam6() //이진화
        {
            Mat src = new Mat("sample.png", ImreadModes.ReducedColor2);
            Mat gray = new Mat();
            Mat binary = new Mat();

            Cv2.CvtColor(src, gray, ColorConversionCodes.BGR2GRAY);
            Cv2.Threshold(gray, binary, 150, 255, ThresholdTypes.Binary);

            Cv2.ImShow("src", src);
            //Cv2.ImShow("gray", gray);
            Cv2.ImShow("dst", binary);
            Cv2.WaitKey(0);
        }
        static void Exam7() //이미지연산1
        {
            Mat src = new Mat("sample.png", ImreadModes.ReducedColor2);
            Mat val = new Mat(src.Size(), MatType.CV_8UC3, new Scalar(0, 0, 30));

            Mat add = new Mat();
            Mat sub = new Mat();
            Mat mul = new Mat();
            Mat div = new Mat();
            Mat max = new Mat();
            Mat min = new Mat();
            Mat abs = new Mat();
            Mat absdiff = new Mat();

            Cv2.Add(src, val, add);
            Cv2.Subtract(src, val, sub);
            Cv2.Multiply(src, val, mul);
            Cv2.Divide(src, val, div);
            Cv2.Max(src, mul, max);
            Cv2.Min(src, mul, min);
            abs = Cv2.Abs(mul);
            Cv2.Absdiff(src, mul, absdiff);

            Cv2.ImShow("add", add);
            Cv2.ImShow("sub", sub);
            Cv2.ImShow("mul", mul);
            Cv2.ImShow("div", div);
            Cv2.ImShow("max", max);
            Cv2.ImShow("min", min);
            Cv2.ImShow("abs", abs);
            Cv2.ImShow("absdiff", absdiff);
            Cv2.WaitKey(0);
        }
        static void Exam8() //이미지연산2
        {
            Mat src1 = new Mat("sample1.png", ImreadModes.ReducedColor2);
            Mat src2 = src1.Flip(FlipMode.Y);

            Mat and = new Mat();
            Mat or = new Mat();
            Mat xor = new Mat();
            Mat not = new Mat();
            Mat compare = new Mat();

            Cv2.BitwiseAnd(src1, src2, and);
            Cv2.BitwiseOr(src1, src2, or);
            Cv2.BitwiseXor(src1, src2, xor);
            Cv2.BitwiseNot(src1, not);
            Cv2.Compare(src1, src2, compare, CmpType.EQ);

            Cv2.ImShow("and", and);
            Cv2.ImShow("or", or);
            Cv2.ImShow("xor", xor);
            Cv2.ImShow("not", not);
            Cv2.ImShow("compare", compare);
            Cv2.WaitKey(0);
        }
        #endregion

        #region 히토그램

        static void Exam9() // 흑백 이미지 히스토그램
        {
            Mat src = Cv2.ImRead("sample.png");
            Mat gray = new Mat();
            Mat hist = new Mat();
            Mat result = Mat.Ones(new Size(256, src.Height), MatType.CV_8UC1);
            Mat dst = new Mat();

            Cv2.CvtColor(src, gray, ColorConversionCodes.BGR2GRAY);
            Cv2.CalcHist(new Mat[] { gray }, new int[] { 0 }, null, hist, 1, new int[] { 256 }, new Rangef[] { new Rangef(0, 256) });
            Cv2.Normalize(hist, hist, 0, 255, NormTypes.MinMax);

            for (int i = 0; i < hist.Rows; i++)
            {
                Cv2.Line(result, new Point(i, src.Height), new Point(i, src.Height - hist.Get<float>(i)), Scalar.White);
            }

            Cv2.ImShow("img", gray);
            Cv2.ImShow("histogram", result);
            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();
        }
        static void Exam10() //컬러 이미지 히스토그램
        {
            Mat src = Cv2.ImRead("sample.png");
            Mat color = new Mat();
            Mat histB = new Mat();
            Mat histG = new Mat();
            Mat histR = new Mat();
            Mat resultB = Mat.Ones(new Size(256, src.Height), MatType.CV_8UC3);
            Mat resultG = Mat.Ones(new Size(256, src.Height), MatType.CV_8UC3);
            Mat resultR = Mat.Ones(new Size(256, src.Height), MatType.CV_8UC3);

            Cv2.CvtColor(src, color, ColorConversionCodes.BGR2BGRA);

            Cv2.CalcHist(new Mat[] { color }, new int[] { 0 }, null, histB, 1, new int[] { 256 }, new Rangef[] { new Rangef(0, 256) });
            Cv2.Normalize(histB, histB, 0, 255, NormTypes.MinMax);

            Cv2.CalcHist(new Mat[] { color }, new int[] { 1 }, null, histG, 1, new int[] { 256 }, new Rangef[] { new Rangef(0, 256) });
            Cv2.Normalize(histG, histG, 0, 255, NormTypes.MinMax);

            Cv2.CalcHist(new Mat[] { color }, new int[] { 2 }, null, histR, 1, new int[] { 256 }, new Rangef[] { new Rangef(0, 256) });
            Cv2.Normalize(histR, histR, 0, 255, NormTypes.MinMax);

            for (int i = 0; i < histB.Rows; i++)
            {
                Cv2.Line(resultB, new Point(i, src.Height), new Point(i, src.Height - histB.Get<float>(i)), Scalar.Blue);
            }
            for (int i = 0; i < histG.Rows; i++)
            {
                Cv2.Line(resultG, new Point(i, src.Height), new Point(i, src.Height - histG.Get<float>(i)), Scalar.Green);
            }
            for (int i = 0; i < histR.Rows; i++)
            {
                Cv2.Line(resultR, new Point(i, src.Height), new Point(i, src.Height - histR.Get<float>(i)), Scalar.Red);
            }

            Cv2.ImShow("img", color);
            Cv2.ImShow("Blue", resultB);
            Cv2.ImShow("Green", resultG);
            Cv2.ImShow("Red", resultR);
            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();
        }

        #endregion

        #region 세그먼트 라벨링
        static void Exam11()
        {
            Mat src = new Mat("sample2.png");
            Mat gray = new Mat();
            Mat binary = new Mat();

            Cv2.CvtColor(src, gray, ColorConversionCodes.BGR2GRAY);
            Cv2.Threshold(gray, binary, 150, 255, ThresholdTypes.Binary);

            Cv2.ImShow("src", src);
            Cv2.ImShow("dst", binary);
            Cv2.WaitKey(0);
        }

        static void Exam12()
        {
            //Mat src = new Mat("sample2.png");
            //Mat bin = new Mat();

            //Cv2.CvtColor(src, bin, ColorConversionCodes.BGR2GRAY);
            //Cv2.Threshold(bin, bin, 0, 255, ThresholdTypes.Binary);

            //Cv2.ImShow("src", src);

            //Mat result = new Mat(src.Size(), MatType.CV_8UC3);
            //CvBlobs blobs = new CvBlobs();
            //blobs.Label(bin);
            //blobs.RenderBlobs(src, result);
            //foreach (var item in blobs)
            //{
            //    CvBlob b = item.Value; Cv2.Circle(result, b.Contour.StartingPoint, 4, Scalar.Red, 2, LineTypes.AntiAlias);
            //    Cv2.PutText(result, b.Label.ToString(), new Point(b.Centroid.X, b.Centroid.Y),
            //        HersheyFonts.HersheyComplex, 1, Scalar.Yellow, 2, LineTypes.AntiAlias);
            //}
            //Cv2.ImShow("result", result); Cv2.WaitKey(0);
        }
        #endregion

        #region Transformation
        static void Exam13() // 이미지 확대 & 축소
        {
            Mat src = Cv2.ImRead("sample1.png");
            Mat dst = new Mat(src.Size(), MatType.CV_8UC3);
            Mat dst2 = new Mat(src.Size(), MatType.CV_8UC3);

            // Cv2.PyrUp(src, dst, new Size(src.Width * 2 + 1, src.Height * 2 - 1));
            Cv2.PyrUp(src, dst);
            Cv2.PyrDown(src, dst2); //4배 축소
            Cv2.PyrDown(dst2, dst2); //16배 축소

            Cv2.ImShow("src", src);
            Cv2.ImShow("dst", dst);
            Cv2.ImShow("dst2", dst2);

            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();
        }
        static void Exam14() // 이미지 크기 조절
        {
            Mat src = Cv2.ImRead("sample1.png");
            Mat dst = new Mat();
            Mat dst2 = new Mat();

            Cv2.Resize(src, dst, new Size(500, 250)); // 절대적 크기
            Cv2.Resize(src, dst2, new Size(0, 0), 0.5, 0.5); // 상대적 크기

            Cv2.ImShow("src", src);
            Cv2.ImShow("dst", dst);
            Cv2.ImShow("dst2", dst2);
            Cv2.WaitKey(0);
        }
        static void Exam15() // 이미지 대칭
        {
            Mat src = Cv2.ImRead("sample.png");
            Mat dst = new Mat(src.Size(), MatType.CV_8UC3);
            Mat dst2 = new Mat(src.Size(), MatType.CV_8UC3);
            Mat dst3 = new Mat(src.Size(), MatType.CV_8UC3);

            Cv2.Flip(src, dst, FlipMode.Y); // Y축대칭
            Cv2.Flip(src, dst2, FlipMode.X); // X축대칭
            Cv2.Flip(src, dst3, FlipMode.XY); // XY축대칭

            Cv2.ImShow("src", src);
            Cv2.ImShow("dst", dst);
            Cv2.ImShow("dst2", dst2);
            Cv2.ImShow("dst3", dst3);
            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();
        }
        static void Exam16() // 이미지 회전
        {
            Mat src = Cv2.ImRead("sample.png");
            Mat dst = new Mat();
            Mat dst2 = new Mat();
            Mat dst3 = new Mat();

            Mat matrix = Cv2.GetRotationMatrix2D(new Point2f(src.Width / 2, src.Height / 2), 90.0, 1.0);
            Mat matrix2 = Cv2.GetRotationMatrix2D(new Point2f(src.Width / 2, src.Height / 2), 180.0, 1.0);
            Mat matrix3 = Cv2.GetRotationMatrix2D(new Point2f(src.Width / 2, src.Height / 2), 45.0, 1.0);

            Cv2.WarpAffine(src, dst, matrix, new Size(src.Width, src.Height)); // 아핀변환 함수를 적용
            Cv2.WarpAffine(src, dst2, matrix2, new Size(src.Width, src.Height));
            Cv2.WarpAffine(src, dst3, matrix3, new Size(src.Width, src.Height));

            Cv2.ImShow("src", src);
            Cv2.ImShow("dst", dst);
            Cv2.ImShow("dst2", dst2);
            Cv2.ImShow("dst3", dst3);

            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();
        }
        static void Exam17() // 아핀 변환
        {
            Mat src = Cv2.ImRead("sample.png");
            Mat dst = new Mat();

            List<Point2f> src_pts = new List<Point2f>()
            {
                new Point2f(0.0f, 0.0f),
                new Point2f(0.0f, src.Height),
                new Point2f(src.Width, src.Height)
            };

            List<Point2f> dst_pts = new List<Point2f>()
            {
               new Point2f(50.0f, 50.0f),
               new Point2f(0.0f, src.Height - 100.0f),
               new Point2f(src.Width - 50.0f, src.Height - 50.0f)
            };

            Mat matrix = Cv2.GetAffineTransform(src_pts, dst_pts);
            Cv2.WarpAffine(src, dst, matrix, new Size(src.Width, src.Height));

            Cv2.ImShow("src", src);
            Cv2.ImShow("dst", dst);
            Cv2.WaitKey(0);
        }
        static void Exam18() // 원근 변환
        {
            Mat src = Cv2.ImRead("sample.png");
            Mat dst = new Mat();

            List<Point2f> src_pts = new List<Point2f>()
            {
                new Point2f(0.0f, 0.0f),
                new Point2f(0.0f, src.Height),
                new Point2f(src.Width, src.Height),
                new Point2f(src.Width, 0.0f)
            };

            List<Point2f> dst_pts = new List<Point2f>()
            {
               new Point2f(50.0f, 50.0f),
               new Point2f(0.0f, src.Height),
               new Point2f(src.Width, src.Height),
               new Point2f(src.Width - 100.0f, 0.0f)
            };

            Mat matrix = Cv2.GetPerspectiveTransform(src_pts, dst_pts);
            Cv2.WarpPerspective(src, dst, matrix, new Size(src.Width, src.Height));

            Cv2.ImShow("src", src);
            Cv2.ImShow("dst", dst);
            Cv2.WaitKey(0);
        }
        #endregion

        #region 라벨링
        static void Exam19() // labeling1
        {
            //Mat src = Cv2.ImRead("sample.png");
            //Mat bin = new Mat();
            //Mat binary = new Mat();

            //src = src.SubMat(new Rect(300, 300, 1000, 1000)); // 이미지 자르기

            //Cv2.CvtColor(src, bin, ColorConversionCodes.BGR2GRAY); // gray
            //Cv2.Threshold(bin, binary, 125, 255, ThresholdTypes.BinaryInv); //이진화

            //Mat result = new Mat(src.Size(), MatType.CV_8UC3);
            //CvBlobs blobs = new CvBlobs();
            //blobs.Label(binary);
            //blobs.RenderBlobs(src, result);

            //foreach (var item in blobs)
            //{
            //    CvBlob b = item.Value;
            //    Cv2.Circle(result, b.Contour.StartingPoint, 4, Scalar.Red, 2, LineTypes.AntiAlias);
            //    Cv2.PutText(result, b.Label.ToString(), new Point(b.Centroid.X, b.Centroid.Y),
            //        HersheyFonts.HersheyComplex, 1, Scalar.Yellow, 2, LineTypes.AntiAlias);
            //}

            //Cv2.ImShow("src", src);
            //Cv2.ImShow("binary", binary);
            //Cv2.ImShow("result", result);
            //Cv2.WaitKey(0);
        }
        static void Exam20() // labeling2
        {
            //Mat src = Cv2.ImRead("sample.png");
            //Mat bin = new Mat();
            //Mat binary = new Mat();

            //src = src.SubMat(new Rect(300, 300, 1000, 1000)); // 이미지 자르기

            //Cv2.CvtColor(src, bin, ColorConversionCodes.BGR2GRAY); // gray
            //Cv2.Threshold(bin, binary, 125, 255, ThresholdTypes.BinaryInv); //이진화

            //Mat result = new Mat(src.Size(), MatType.CV_8UC3);
            //CvBlobs blobs = new CvBlobs();

            //blobs.Label(binary);
            //blobs.RenderBlobs(src, result);

            //int text = 1; // 번호
            //foreach (var item in blobs)
            //{
            //    if (item.Value.Area > 40000) // 라벨링 면적 확인
            //    {
            //        CvBlob b = item.Value;

            //        Cv2.Circle(result, b.Contour.StartingPoint, 8, Scalar.Red, 2, LineTypes.AntiAlias);
            //        Cv2.PutText(result, text.ToString(), new Point(b.Centroid.X, b.Centroid.Y),  // 라벨링 번호 설정 수정
            //            HersheyFonts.HersheyComplex, 1, Scalar.Yellow, 2, LineTypes.AntiAlias);
            //        text++;
            //    }
            //}

            //Cv2.ImShow("src", src);
            //Cv2.ImShow("binary", binary);
            //Cv2.ImShow("result", result);
            //Cv2.WaitKey(0);
        }
        #endregion


        static void Main(string[] args)
        {
            Exam20();
        }
    }
}
