using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dimensional
{
    public abstract class Dimension
    {   
    }

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

    public class Length : Dimension
    {
    }

    public class Time : Dimension
    {
    }

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
            var tenUnitLengths = new Quantity<Length>(10f);

            var fiveUnitLengths = new Quantity<Length>(5f);

            var twoUnitsOfTime = new Quantity<Time>(2f);

            //Test: Addition
            var fifteenLengthUnits = tenUnitLengths.Add(fiveUnitLengths);

            var fourUnitsOfTime = twoUnitsOfTime.Add(twoUnitsOfTime);

            //var cantAddLengthToTime = tenUnitLengths.Add(twoUnitsOfTime);

            //Test: Multiplication
            var hundredLengthUnitsSquared = tenUnitLengths.MultipliedBy(tenUnitLengths);

            var thirtyLengthTimeUnits = fifteenLengthUnits.MultipliedBy(twoUnitsOfTime);

            //var cantAddProductToSingleDimensionQuantity = thirtyMetreSeconds.Add(fourUnitsOfTime);

            //Test: Division

            var fiveLengthPerTimeUnits = tenUnitLengths.DivideBy(twoUnitsOfTime);

            var twentyLengthUnitsDodgy = fiveLengthPerTimeUnits.MultipliedBy(new Quantity<Time>(4f));


            //Now, it would be nice if these were to compile, thus removing the Product / Quotient constructors:

            /*
            Quantity<Length> twentyLengthUnitsNice = hundredLengthUnitsSquared.DivideBy(fiveUnitLengths);

            Quantity<Length> twentyLengthUnitsAlsoNice = fiveLengthPerTimeUnits.MultipliedBy(new Quantity<Time>(4f));
            */
        }
    }
}
