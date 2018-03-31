using System;
using System.Threading.Tasks;

namespace Core.Helpers
{
    public static class ExecuteAndHandleException<T>
    {
        public static async Task<T> Execute(Func<Task<T>> func)
        {
            try
            {
                return await func();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }


    public static class ExecuteAndHandleException
    {
        public static async Task Execute(Func<Task> func)
        {
            try
            {
                await func();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}