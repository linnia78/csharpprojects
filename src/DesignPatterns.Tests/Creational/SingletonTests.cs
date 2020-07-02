using System;
using Xunit;

namespace DesignPatterns.Tests.Structural
{
    public class SingletonTests
    {
        /* 
            Pattern     :   
            References  :
                            https://refactoring.guru/design-patterns/singleton
                            https://sourcemaking.com/design_patterns/singleton
            Real World  :   
                            
            Relations   :   
                            
        */

        public class DoubleCheckLockingSingleton // Thread Safety
        {
            DoubleCheckLockingSingleton()
            {
            }
            private static readonly object padlock = new object();
            private static DoubleCheckLockingSingleton instance = null;
            public static DoubleCheckLockingSingleton Instance
            {
                get
                {
                    if (instance == null)
                    {
                        lock (padlock)
                        {
                            if (instance == null)
                            {
                                instance = new DoubleCheckLockingSingleton();
                            }
                        }
                    }
                    return instance;
                }
            }
        }


        public class StaticConstructorSingleton // This type of implementation has a static constructor, so it executes only once per Application Domain
        {
            private static readonly StaticConstructorSingleton instance = new StaticConstructorSingleton();
            // Explicit static constructor to tell C# compiler  
            // not to mark type as beforefieldinit  
            static StaticConstructorSingleton()
            {
            }
            private StaticConstructorSingleton()
            {
            }
            public static StaticConstructorSingleton Instance
            {
                get
                {
                    return instance;
                }
            }
        }

        public sealed class LazySingleton // Lazy singleton
        {
            private LazySingleton()
            {
            }
            private static readonly Lazy<LazySingleton> lazy = new Lazy<LazySingleton>(() => new LazySingleton());
            public static LazySingleton Instance
            {
                get
                {
                    return lazy.Value;
                }
            }
        }
    }
}
