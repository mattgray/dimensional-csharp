namespace Dimensional
{
    //dimension base type
    public abstract class Dimension
    {   
    }

    //A couple of dimensions to be getting started with
    
    public class Length : Dimension
    {
    }

    public class Time : Dimension
    {
    }

    //product and quotient

    public class Product<TDimension1, TDimension2> : Dimension
        where TDimension1 : Dimension
        where TDimension2 : Dimension
    {
    }

    public class Quotient<TDividend, TDivisor> : Dimension
        where TDividend : Dimension
        where TDivisor : Dimension
    {
    }

    //A given quantity of a certain dimension

    public class Quantity<TDimension>  where TDimension : Dimension
    {
        private readonly float _amount;

        public Quantity(float amount)
        {
            _amount = amount;
        }

        public Quantity<TDimension> Add(Quantity<TDimension> toAdd)
        {
            return new Quantity<TDimension>(_amount + toAdd._amount);
        }

        public Quantity<TDimension> Subtract(Quantity<TDimension> toSubtract)
        {
            return new Quantity<TDimension>(_amount - toSubtract._amount);
        }

        public Quantity<Product<TDimension, TDimension2>> MultipliedBy<TDimension2>(Quantity<TDimension2> toMultiplyBy) 
            where TDimension2 : Dimension
        {
            return new Quantity<Product<TDimension, TDimension2>>(_amount * toMultiplyBy._amount);
        }

        public Quantity<Quotient<TDimension, TDivisor>> DivideBy<TDivisor>(Quantity<TDivisor> divisor)
            where TDivisor : Dimension
        {
            return new Quantity<Quotient<TDimension, TDivisor>>(_amount / divisor._amount);
        }
    }

    public class Scratch
    {
        public void Blah()
        {
            //Test: Quantity construction
            var tenUnitLengths = new Quantity<Length>(10);

            var fiveUnitLengths = new Quantity<Length>(5);

            var twoUnitsOfTime = new Quantity<Time>(2);

            //Test: Addition
            var fifteenLengthUnits = tenUnitLengths.Add(fiveUnitLengths);

            var fourUnitsOfTime = twoUnitsOfTime.Add(twoUnitsOfTime);

            //var cantAddLengthToTime = tenUnitLengths.Add(twoUnitsOfTime);

            //Test: Subtraction

            var threeUnitsOfTime = fourUnitsOfTime.Subtract(new Quantity<Time>(1));

            //Test: Multiplication
            var hundredLengthUnitsSquared = tenUnitLengths.MultipliedBy(tenUnitLengths);

            var thirtyLengthTimeUnits = fifteenLengthUnits.MultipliedBy(twoUnitsOfTime);

            //var cantAddProductToSingleDimensionQuantity = thirtyMetreSeconds.Add(fourUnitsOfTime);

            //Test: Division

            var fiveLengthPerTimeUnits = tenUnitLengths.DivideBy(twoUnitsOfTime);

            var twentyLengthUnitsDodgy = fiveLengthPerTimeUnits.MultipliedBy(new Quantity<Time>(4));
                      
            //Now, it would be nice if these were to compile, removing the nested Product / Quotient constructors:

            /*
            Quantity<Length> twentyLengthUnitsNice = hundredLengthUnitsSquared.DivideBy(fiveUnitLengths);

            Quantity<Length> twentyLengthUnitsAlsoNice = fiveLengthPerTimeUnits.MultipliedBy(new Quantity<Time>(4));
            */
        }
    }
}
