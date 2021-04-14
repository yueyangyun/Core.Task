using System;
using System.Threading;

namespace Core.Task
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Dick 2021-04-09 [ 场景一，真异步 ]
            Console.WriteLine("主线程开始");
            GetTaskAsync();
            Console.WriteLine("主线程结束");

            Console.WriteLine("=================================分割线===================================");

            // Dick 2021-04-09 [ 场景二，假异步 ]
            //Console.WriteLine("主线程开始");
            //System.Threading.Tasks.Task<int> n = TaskAsync();
            //Console.WriteLine(n.Result); //立即获取异步执行结果，如果还没有结果，则会继续等待，从而失去异步效果
            //Console.WriteLine("主线程结束");

            Console.ReadKey();
        }

        /// <summary>
        /// 执行几个耗时操作的时候，用异步去分别执行这几个操作，
        /// 注意，不要在执行异步操作的下一句代码立马去用它的结果，否则就会造成等待完成，失去异步效果。
        /// </summary>
        /// <returns></returns>
        public static async System.Threading.Tasks.Task<int> TaskAsync()
        {
            Console.WriteLine("异步方法开始");
            // await，表示需要开一个线程去执行await后面的内容，主线程立马跳出了该方法继续往下执行
            int n = await System.Threading.Tasks.Task.Run(() =>
             {
                 Thread.Sleep(1000);
                 Console.WriteLine("异步线程执行");
                 return 3;
             });
            Console.WriteLine("异步方法结束");
            return n;
        }

        public static async System.Threading.Tasks.Task GetTaskAsync()
        {
            Console.WriteLine("异步方法开始");
             await System.Threading.Tasks.Task.Run(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("异步线程执行");
            });
            Console.WriteLine("异步方法结束");
        }
    }
}
