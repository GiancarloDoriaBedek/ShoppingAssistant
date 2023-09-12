namespace ShoppingAssistant.Services.Interfaces
{
    public interface IStringDistanceService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>At some point replace with faster implementation https://github.com/Turnerj/Quickenshtein</remarks>
        /// <param name="firstString"></param>
        /// <param name="secondString"></param>
        /// <returns></returns>
        int CalculateLevenshteinDistance(string firstString, string secondString);
    }
}
