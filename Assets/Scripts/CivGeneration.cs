using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivGeneration
{
    int civCount = 0;
    float civChance;
    int numOfCivs;

    public CivGeneration(int numOfCivs, float civChance) {
        this.numOfCivs = numOfCivs;
        this.civChance = civChance;
    }

    public bool ShouldGenerateTown() {
        bool generateTown = false;
        if(civCount < this.numOfCivs) {
            float roll = Random.Range(0, 10000);
            if(roll < civChance) {
                generateTown = true;
                this.civCount++;
            }
        }
        return generateTown;
    }

    public Color[] GenerateTowns(Color[] colorMap, int index, int citySprawl, int mapWidth) {
        colorMap[index] = Color.red;
        colorMap[index + Random.Range(0, citySprawl)] = Color.red;
        colorMap[index - Random.Range(0, citySprawl)] = Color.red;
        colorMap[index + Random.Range(0, citySprawl) * mapWidth] = Color.red;
        colorMap[index - Random.Range(0, citySprawl) * mapWidth] = Color.red;

        colorMap[index + Random.Range(0, citySprawl) * mapWidth + Random.Range(0, citySprawl)] = Color.red;
        colorMap[index + Random.Range(0, citySprawl) * mapWidth - Random.Range(0, citySprawl)] = Color.red;

        colorMap[index - Random.Range(0, citySprawl) * mapWidth + Random.Range(0, citySprawl)] = Color.red;
        colorMap[index - Random.Range(0, citySprawl) * mapWidth - Random.Range(0, citySprawl)] = Color.red;


        return colorMap;
    }
}
