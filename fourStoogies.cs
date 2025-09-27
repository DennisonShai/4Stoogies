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

public class fourStoogies : Bot, ISpinBot
{
    static void Main(string[] args)
    {
        new fourStoogies().Start();
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
            // move faaaaaaaaaaaaast
            MaxSpeed = 10;
            Forward(10_000);
        }
    }




    //shoots rammed bot
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




    public override void OnScannedBot(ScannedBotEvent e)
    {
        // finds bot
        var bearingFromGun = GunBearingTo(e.X, e.Y);

        // turn toward target
        TurnGunLeft(bearingFromGun);

        //fires if near
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

        ;
    }

}