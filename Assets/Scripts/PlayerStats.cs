using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

//a really basic inventory testing character
public class PlayerStats : MonoBehaviour {
    public int health, maxhealth, speed, stamina, poise, lives;
    public void takeDamage(int i)
    {
        health -= i;
        if(health <= 0)
        {
            if(lives > 0)
            {
                health = maxhealth;
                lives--;
            }
            else
            {
                Debug.Log("The player has died!");
            }
        }
    }
    public void IncreaseStat(string statName, int amount)
    {
        //increases the property (find a better way to do this god damn)
        this.GetType().GetProperty(statName).SetValue(this,((int)this.GetType().GetProperty(statName).GetValue(this,null) + amount), null);
    }
    public void printMessage(int active)
    {
        Debug.Log("A message has been printed!");
    }
}
