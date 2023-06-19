using System;
using System.Collections.Generic;
namespace FinancialAnalysis
{
 // Class to represent a financial asset
 public class Asset
 {
 public string Symbol { get; set; }
 public DateTime Date { get; set; }
 public double Price { get; set; }
 public Asset(string symbol, DateTime date, double price)
 {
 Symbol = symbol;
 Date = date;
 Price = price;
 }
 }
 // Class to calculate financial metrics
 public static class FinancialMetricsCalculator
 {
 // Calculate returns for a given list of assets
 public static List<double> CalculateReturns(List<Asset> assets)
 {
 List<double> returns = new List<double>();
 for (int i = 1; i < assets.Count; i++)
 {
 double currentPrice = assets[i].Price;
 double previousPrice = assets[i - 1].Price;
 double assetReturn = (currentPrice - previousPrice) / previousPrice;
 returns.Add(assetReturn);
 }
 return returns;
 }
 // Calculate volatility for a given list of returns
 public static double CalculateVolatility(List<double> returns)
 {
 double sumOfSquares = 0;
 foreach (double assetReturn in returns)
 {
 sumOfSquares += Math.Pow(assetReturn, 2);
 }
 double variance = sumOfSquares / returns.Count;
 double volatility = Math.Sqrt(variance);
 return volatility;
 }
 // Calculate correlation between two lists of returns
 public static double CalculateCorrelation(List<double> returns1, List<double> returns2)
 {
 if (returns1.Count != returns2.Count)
 {
 throw new ArgumentException("Returns lists must have the same length.");
 }
 double sum1 = 0;
 double sum2 = 0;
 double sumProduct = 0;
 double sumSquared1 = 0;
 double sumSquared2 = 0;
 for (int i = 0; i < returns1.Count; i++)
 {
 double return1 = returns1[i];
 double return2 = returns2[i];
 sum1 += return1;
 sum2 += return2;
 sumProduct += return1 * return2;
 sumSquared1 += Math.Pow(return1, 2);
 sumSquared2 += Math.Pow(return2, 2);
 }
 double correlation =
 (returns1.Count * sumProduct - sum1 * sum2) /
 Math.Sqrt((returns1.Count * sumSquared1 - Math.Pow(sum1, 2)) *
 (returns1.Count * sumSquared2 - Math.Pow(sum2, 2)));
 return correlation;
 }
 }
 // Class to demonstrate the program
 public class Program
 {
 public static void Main(string[] args)
 {
 // Sample asset data
 List<Asset> assets = new List<Asset>
 {
 new Asset("AAPL", new DateTime(2022, 1, 1), 150.0),
 new Asset("AAPL", new DateTime(2022, 1, 2), 152.0),
 new Asset("AAPL", new DateTime(2022, 1, 3), 155.0),
 new Asset("AAPL", new DateTime(2022, 1, 4), 148.0),
 new Asset("AAPL", new DateTime(2022, 1, 5), 150.0)
 };
 // Filter assets for a specific time period
 List<Asset> filteredAssets = FilterAssetsByDate(assets, new DateTime(2022, 1, 3), new DateTime(2022, 1,
5));
 // Calculate returns for the filtered assets
 List<double> returns = FinancialMetricsCalculator.CalculateReturns(filteredAssets);
 // Calculate volatility
 double volatility = FinancialMetricsCalculator.CalculateVolatility(returns);
 // Calculate correlation between two assets
 List<Asset> assets2 = new List<Asset>
 {
 new Asset("GOOGL", new DateTime(2022, 1, 1), 2000.0),
 new Asset("GOOGL", new DateTime(2022, 1, 2), 2010.0),
 new Asset("GOOGL", new DateTime(2022, 1, 3), 1990.0),
 new Asset("GOOGL", new DateTime(2022, 1, 4), 2020.0),
 new Asset("GOOGL", new DateTime(2022, 1, 5), 2030.0)
 };
 List<double> returns2 = FinancialMetricsCalculator.CalculateReturns(assets2);
 double correlation = FinancialMetricsCalculator.CalculateCorrelation(returns, returns2);
 // Print results
 Console.WriteLine("Returns:");
 foreach (double assetReturn in returns)
 {
 Console.WriteLine(assetReturn);
 }
 Console.WriteLine("Volatility: " + volatility);
 Console.WriteLine("Correlation: " + correlation);
 }
 // Filter assets by date range
 public static List<Asset> FilterAssetsByDate(List<Asset> assets, DateTime startDate, DateTime endDate)
 {
 List<Asset> filteredAssets = new List<Asset>();
 foreach (Asset asset in assets)
 {
 if (asset.Date >= startDate && asset.Date <= endDate)
 {
 filteredAssets.Add(asset);
 }
 }
 return filteredAssets;
 }
 // Unit tests
 public static void RunUnitTests()
 {
 // Test CalculateReturns
 List<Asset> testAssets = new List<Asset>
 {
 new Asset("A", new DateTime(2022, 1, 1), 10),
 new Asset("A", new DateTime(2022, 1, 2), 12),
 new Asset("A", new DateTime(2022, 1, 3), 8),
 new Asset("A", new DateTime(2022, 1, 4), 11)
 };
 List<double> expectedReturns = new List<double> { 0.2, -0.3333333333333333, 0.375 };
 List<double> actualReturns = FinancialMetricsCalculator.CalculateReturns(testAssets);
 // Compare expected and actual returns
 for (int i = 0; i < expectedReturns.Count; i++)
 {
 if (Math.Abs(expectedReturns[i] - actualReturns[i]) > 0.0001)
 {
 throw new Exception("CalculateReturns test failed.");
 }
 }
 // Test CalculateVolatility
 List<double> testReturns = new List<double> { 0.1, 0.2, -0.1, 0.3, 0.05 };
 double expectedVolatility = 0.151745751;
 double actualVolatility = FinancialMetricsCalculator.CalculateVolatility(testReturns);
 // Compare expected and actual volatility
 if (Math.Abs(expectedVolatility - actualVolatility) > 0.0001)
 {
 throw new Exception("CalculateVolatility test failed.");
 }
 // Test CalculateCorrelation
 List<double> testReturns2 = new List<double> { 0.05, 0.15, -0.05, 0.25, 0.1 };
 double expectedCorrelation = 0.993772287;
 double actualCorrelation = FinancialMetricsCalculator.CalculateCorrelation(testReturns, testReturns2);
 // Compare expected and actual correlation
 if (Math.Abs(expectedCorrelation - actualCorrelation) > 0.0001)
 {
 throw new Exception("CalculateCorrelation test failed.");
 }
 }
 }
}