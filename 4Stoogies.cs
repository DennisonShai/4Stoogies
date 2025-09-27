using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;
using Robocode.TankRoyale.BotApi.Graphics;

using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;

public interface ISpinBot
{
    void OnHitBot(HitBotEvent e);
    void OnScannedBot(ScannedBotEvent evt);
    void Run();
}

public class SpinBot : Bot, ISpinBot
{
    static void Main(string[] args)
    {
        new SpinBot().Start();
    }
    public override void Run()
    {
        BodyColor = Color.Pink;
        TurretColor = Color.Orange;
        RadarColor = Color.Pink;
        ScanColor = Color.Black;

       
        while (IsRunning)
        {
            
            SetTurnRight(10_000);
            // Limit our speed to 5
            MaxSpeed = 5;
            // Start moving (and turning)
            Forward(10_000);
        }
    }

   

    // We hit another bot -> if it's our fault, we'll stop turning and moving,
    // so we need to turn again to keep spinning.
    public override void OnHitBot(HitBotEvent e)
    {
        var bearing = BearingTo(e.X, e.Y);
        if (bearing > -10 && bearing < 10)
        {
            Fire(3);
        }
        if (e.IsRammed)
        {
            TurnRight(10);
        }
    }
};



public override void OnScannedBot(ScannedBotEvent e)
{
    // Calculate direction of the scanned bot and bearing to it for the gun
    var bearingFromGun = GunBearingTo(e.X, e.Y);

    // Turn the gun toward the scanned bot
    TurnGunLeft(bearingFromGun);

    // If it is close enough, fire!
    var distance = DistanceTo(e.X, e.Y);
    if (distance < 50)
    {
        Fire(2);
    }
    else
    {
        // Otherwise, only fire 1
        Fire(1);
    }
    if (bearingFromGun == 0)
        Rescan();

}
public override void OnScannedBot(ScannedBotEvent e)
{
    //fires hard at nearby enemies
    
    // Rescan
    Rescan();
}
