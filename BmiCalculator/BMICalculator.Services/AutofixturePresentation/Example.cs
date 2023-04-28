using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMICalculator.Services.AutofixturePresentation;

public class Example
{
    private IDependency a;
    private IDependency b;

    public Example(IDependency one, IDependency two)
    {
        a = one ?? throw new ArgumentNullException(nameof(one));
        b = two ?? throw new ArgumentNullException(nameof(two));
    }
}

public class AnotherExpample
{
    private readonly IDependency one;
    private readonly IDependency two;

    public AnotherExpample(IDependency one, IDependency two)
    {
        this.one = one;
        this.two = two;
    }
}

public interface IDependency
{
}

public class Dependency : IDependency
{

}
