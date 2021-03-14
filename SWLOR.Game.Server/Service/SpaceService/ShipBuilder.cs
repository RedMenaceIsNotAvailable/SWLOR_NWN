﻿using System.Collections.Generic;
using SWLOR.Game.Server.Core.NWScript.Enum;
using SWLOR.Game.Server.Enumeration;

namespace SWLOR.Game.Server.Service.SpaceService
{
    public class ShipBuilder
    {
        private readonly Dictionary<ShipType, ShipDetail> _ships = new Dictionary<ShipType, ShipDetail>();
        private ShipDetail _activeShip;

        /// <summary>
        /// Creates a new ship.
        /// </summary>
        /// <param name="type">The type of ship to associate with this detail.</param>
        /// <returns>A ship builder with the configured options.</returns>
        public ShipBuilder Create(ShipType type)
        {
            _activeShip = new ShipDetail();
            _ships[type] = _activeShip;

            return this;
        }

        /// <summary>
        /// Sets the name of the ship currently being built.
        /// </summary>
        /// <param name="name">The name to set.</param>
        /// <returns>A ship builder with the configured options.</returns>
        public ShipBuilder Name(string name)
        {
            _activeShip.Name = name;

            return this;
        }

        /// <summary>
        /// Sets the appearance of the ship.
        /// The player's appearance will be changed to this when they enter space mode.
        /// </summary>
        /// <param name="appearance">The appearance to set.</param>
        /// <returns>A ship builder with the configured options.</returns>
        public ShipBuilder Appearance(AppearanceType appearance)
        {
            _activeShip.Appearance = appearance;

            return this;
        }

        /// <summary>
        /// Sets the maximum shields of the ship.
        /// </summary>
        /// <param name="maxShield">The value to set.</param>
        /// <returns>A ship builder with the configured options.</returns>
        public ShipBuilder MaxShield(int maxShield)
        {
            _activeShip.MaxShield = maxShield;

            return this;
        }

        /// <summary>
        /// Sets the maximum armor of the ship.
        /// </summary>
        /// <param name="maxArmor">The value to set.</param>
        /// <returns>A ship builder with the configured options.</returns>
        public ShipBuilder MaxArmor(int maxArmor)
        {
            _activeShip.MaxHull = maxArmor;

            return this;
        }

        /// <summary>
        /// Sets the maximum capacitor of the ship.
        /// </summary>
        /// <param name="maxCapacitor">The value to set.</param>
        /// <returns>A ship builder with the configured options.</returns>
        public ShipBuilder MaxCapacitor(int maxCapacitor)
        {
            _activeShip.MaxCapacitor = maxCapacitor;

            return this;
        }

        /// <summary>
        /// Sets the shield recharge rate of the ship.
        /// </summary>
        /// <param name="shieldRechargeRate">The value to set.</param>
        /// <returns>A ship builder with the configured options.</returns>
        public ShipBuilder ShieldRechargeRate(float shieldRechargeRate)
        {
            _activeShip.ShieldRechargeRate = shieldRechargeRate;

            return this;
        }

        /// <summary>
        /// Sets the number of high power nodes on this ship.
        /// High power nodes are typically used for weaponry and mining lasers.
        /// </summary>
        /// <param name="highPowerNodes">The number of high power nodes to set.</param>
        /// <returns>A ship builder with the configured options.</returns>
        public ShipBuilder HighPowerNodes(int highPowerNodes)
        {
            _activeShip.HighPowerNodes = highPowerNodes;

            return this;
        }

        /// <summary>
        /// Sets the number of low power nodes on this ship.
        /// Low power nodes are typically used for shield boosters, armor reinforcement, etc.
        /// </summary>
        /// <param name="lowPowerNodes">The number of low power nodes to set.</param>
        /// <returns>A ship builder with the configured options.</returns>
        public ShipBuilder LowPowerNodes(int lowPowerNodes)
        {
            _activeShip.LowPowerNodes = lowPowerNodes;

            return this;
        }

        /// <summary>
        /// Indicates the ship has a bay in which a droid can be installed.
        /// </summary>
        /// <returns>A ship builder with the configured options.</returns>
        public ShipBuilder HasDroidBay()
        {
            _activeShip.HasDroidBay = true;

            return this;
        }

        /// <summary>
        /// Returns a built dictionary of ships.
        /// </summary>
        /// <returns>A dictionary of ship details.</returns>
        public Dictionary<ShipType, ShipDetail> Build()
        {
            return _ships;
        }
    }
}
