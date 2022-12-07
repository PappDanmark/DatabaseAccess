using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Papp.Domain;

namespace Papp.Persistence.Context
{
    public partial class PappDbContext : DbContext
    {
        public PappDbContext()
        {
        }

        public PappDbContext(DbContextOptions<PappDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Booth> Booths { get; set; } = null!;
        public virtual DbSet<Bundle> Bundles { get; set; } = null!;
        public virtual DbSet<Charger> Chargers { get; set; } = null!;
        public virtual DbSet<ChargerConnector> ChargerConnectors { get; set; } = null!;
        public virtual DbSet<ChargerType> ChargerTypes { get; set; } = null!;
        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<Image> Images { get; set; } = null!;
        public virtual DbSet<Manufacturer> Manufacturers { get; set; } = null!;
        public virtual DbSet<Operator> Operators { get; set; } = null!;
        public virtual DbSet<ParkingArea> ParkingAreas { get; set; } = null!;
        public virtual DbSet<ParkingAreaTransaction> ParkingAreaTransactions { get; set; } = null!;
        public virtual DbSet<ParkingBooth> ParkingBooths { get; set; } = null!;
        public virtual DbSet<ParkingBundle> ParkingBundles { get; set; } = null!;
        public virtual DbSet<LegacySensor> LegacySensors { get; set; } = null!;
        public virtual DbSet<Sensor> Sensors { get; set; } = null!;
        public virtual DbSet<SensorActionOccupied> SensorActionOccupieds { get; set; } = null!;
        public virtual DbSet<SensorActionsRaw> SensorActionsRaws { get; set; } = null!;
        public virtual DbSet<SensorBatteryUpdate> SensorBatteryUpdates { get; set; } = null!;
        public virtual DbSet<SensorInstall> SensorInstalls { get; set; } = null!;
        public virtual DbSet<SensorType> SensorTypes { get; set; } = null!;
        public virtual DbSet<SensorUpdate> SensorUpdates { get; set; } = null!;
        public virtual DbSet<ZipCode> ZipCodes { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp");

            modelBuilder.Entity<Booth>(entity =>
            {
                entity.ToTable("booth");

                entity.HasComment("A parking booth.");

                entity.HasIndex(e => e.Id, "booth_id_uindex")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.BoothNumber)
                    .HasColumnName("booth_number")
                    .HasComment("Which number this booth has in its bundle.");

                entity.Property(e => e.Bundle)
                    .HasColumnName("bundle")
                    .HasComment("Referencing the bundle this booth is part of.");

                entity.Property(e => e.Charger)
                    .HasColumnName("charger")
                    .HasComment("If null then this booth does not have an associated charger.");

                entity.Property(e => e.CraftsmenExclusiveOh)
                    .HasColumnType("character varying")
                    .HasColumnName("craftsmen_exclusive_oh")
                    .HasComment("Following the OpeningHours standard. If null this is not exclusive for craftsmen.");

                entity.Property(e => e.ElectricExclusiveOh)
                    .HasColumnType("character varying")
                    .HasColumnName("electric_exclusive_oh")
                    .HasComment("Following the OpeningHours standard. If null this is not electric exclusive.");

                entity.Property(e => e.HandicapOh)
                    .HasColumnType("character varying")
                    .HasColumnName("handicap_oh")
                    .HasComment("Following the OpeningHours standard. If null this is not a handicap booth.");

                entity.Property(e => e.MuncipalityId)
                    .HasColumnType("character varying")
                    .HasColumnName("muncipality_id")
                    .HasComment("The muncipality ID of this booth.");

                entity.Property(e => e.SensorInstall)
                    .HasColumnName("sensor_install")
                    .HasComment("Referencing the sensor installed, and if null then there's no sensor installed.");

                entity.HasOne(d => d.BundleNavigation)
                    .WithMany(p => p.Booths)
                    .HasForeignKey(d => d.Bundle)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("booth_bundle_id_fk");

                entity.HasOne(d => d.ChargerNavigation)
                    .WithMany(p => p.Booths)
                    .HasForeignKey(d => d.Charger)
                    .HasConstraintName("booth_charger_id_fk");

                entity.HasOne(d => d.SensorInstallNavigation)
                    .WithMany(p => p.Booths)
                    .HasForeignKey(d => d.SensorInstall)
                    .HasConstraintName("booth_sensor_install_id_fk");
            });

            modelBuilder.Entity<Bundle>(entity =>
            {
                entity.ToTable("bundle");

                entity.HasIndex(e => e.Id, "bundle_id_uindex")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("address");

                entity.Property(e => e.Location).HasColumnName("location");

                entity.Property(e => e.Zip).HasColumnName("zip");

                entity.HasOne(d => d.ZipNavigation)
                    .WithMany(p => p.Bundles)
                    .HasForeignKey(d => d.Zip)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bundle_zip_code_id_fk");
            });

            modelBuilder.Entity<Charger>(entity =>
            {
                entity.ToTable("charger");

                entity.HasIndex(e => e.Id, "charger_id_uindex")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.ChargerType)
                    .HasColumnName("charger_type")
                    .HasComment("References the charger type.");

                entity.Property(e => e.OperatorId)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("operator_id")
                    .HasComment("The operators ID of this charging station.");

                entity.HasOne(d => d.ChargerTypeNavigation)
                    .WithMany(p => p.Chargers)
                    .HasForeignKey(d => d.ChargerType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("charger_charger_type_id_fk");
            });

            modelBuilder.Entity<ChargerConnector>(entity =>
            {
                entity.ToTable("charger_connector");

                entity.HasComment("Contains all connector types.");

                entity.HasIndex(e => e.Id, "charger_connector_id_uindex")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("name");
            });

            modelBuilder.Entity<ChargerType>(entity =>
            {
                entity.ToTable("charger_type");

                entity.HasIndex(e => e.Id, "charger_type_id_uindex")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Connector).HasColumnName("connector");

                entity.Property(e => e.Dc)
                    .HasColumnName("dc")
                    .HasComment("Whether the charger is DC or AC.");

                entity.Property(e => e.Kilowatt)
                    .HasColumnName("kilowatt")
                    .HasComment("The charger capacity.");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("name")
                    .HasComment("The name/model of the charger type.");

                entity.Property(e => e.Operator).HasColumnName("operator");

                entity.HasOne(d => d.ConnectorNavigation)
                    .WithMany(p => p.ChargerTypes)
                    .HasForeignKey(d => d.Connector)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("charger_type_charger_connector_id_fk");

                entity.HasOne(d => d.OperatorNavigation)
                    .WithMany(p => p.ChargerTypes)
                    .HasForeignKey(d => d.Operator)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("charger_type_operator_id_fk");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.Iso3166Numeric)
                    .HasName("country_pk");

                entity.ToTable("country");

                entity.HasIndex(e => e.Iso3166Alpha3, "country_iso-3166-alpha-3_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.Iso3166Numeric, "country_iso-3166-numeric_uindex")
                    .IsUnique();

                entity.Property(e => e.Iso3166Numeric)
                    .ValueGeneratedNever()
                    .HasColumnName("iso-3166-numeric");

                entity.Property(e => e.AreaKm2).HasColumnName("area_km2");

                entity.Property(e => e.CommonName)
                    .IsRequired()
                    .HasColumnName("common_name");

                entity.Property(e => e.Iso3166Alpha2)
                    .IsRequired()
                    .HasColumnName("iso-3166-alpha-2");

                entity.Property(e => e.Iso3166Alpha3)
                    .IsRequired()
                    .HasColumnName("iso-3166-alpha-3");

                entity.Property(e => e.OfficialName)
                    .IsRequired()
                    .HasColumnName("official_name");

                entity.Property(e => e.Population).HasColumnName("population");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.ToTable("image");

                entity.HasComment("Single table for images.");

                entity.HasIndex(e => e.Id, "image_id_uindex")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CompressionType)
                    .HasColumnType("character varying")
                    .HasColumnName("compression_type")
                    .HasComment("e.g. 'zip', 'rar', 'gzip' etc. If null then the bytea is not compressed.");

                entity.Property(e => e.Data)
                    .IsRequired()
                    .HasColumnName("data")
                    .HasComment("The raw data associated with this image.");

                entity.Property(e => e.MimeType)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("mime_type")
                    .HasComment("e.g. 'image/jpeg', 'image/png' etc.");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Manufacturer>(entity =>
            {
                entity.ToTable("manufacturer");

                entity.HasComment("This table contains all the sensor producing companies.");

                entity.HasIndex(e => e.Id, "manufacturer_id_uindex")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('\"Manufacturer_id_seq\"'::regclass)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Operator>(entity =>
            {
                entity.ToTable("operator");

                entity.HasComment("The charger operator.");

                entity.HasIndex(e => e.Id, "operator_id_uindex")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("name")
                    .HasComment("Name of the charger operator.");
            });

            modelBuilder.Entity<ParkingArea>(entity =>
            {
                entity.ToTable("parking_area");

                entity.HasComment("Table that contains parking areas like parking houses, which aren't for a specific kind of car or person.");

                entity.HasIndex(e => e.Id, "parking_area_id_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.PappId, "parking_area_papp_id_uindex")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Coordinates)
                    .HasColumnName("coordinates")
                    .HasComment("The coordinate set for the placement of the ParkingArea on a map.");

                entity.Property(e => e.CoordinatesEntry)
                    .HasColumnName("coordinates_entry")
                    .HasComment("The coordinate set for the entry to the parking area.");

                entity.Property(e => e.LatestOccupiedSpaces)
                    .HasColumnName("latest_occupied_spaces")
                    .HasComment("The number of occupied spaces from the latest update. Null if it hasnt been updated or error in the DB.");

                entity.Property(e => e.LatestOccupiedTimestamp)
                    .HasColumnName("latest_occupied_timestamp")
                    .HasComment("The timestamp of the latest occupied_spaces update. Null if none or error in DB.");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.PappId)
                    .IsRequired()
                    .HasColumnName("papp_id");

                entity.Property(e => e.SensorTypeId).HasColumnName("sensor_type_id");

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasColumnName("street")
                    .HasComment("The street name and number.");

                entity.Property(e => e.TotalSpaces).HasColumnName("total_spaces");

                entity.Property(e => e.ZipCodeId).HasColumnName("zip_code_id");

                entity.HasOne(d => d.SensorType)
                    .WithMany(p => p.ParkingAreas)
                    .HasForeignKey(d => d.SensorTypeId)
                    .HasConstraintName("parking_area_sensor_types_id_fk");

                entity.HasOne(d => d.ZipCode)
                    .WithMany(p => p.ParkingAreas)
                    .HasForeignKey(d => d.ZipCodeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("parking_area_zip_code_id_fk");
            });

            modelBuilder.Entity<ParkingAreaTransaction>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.ParkingAreaId })
                    .HasName("parking_area_transaction_pkey");

                entity.ToTable("parking_area_transaction");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.ParkingAreaId).HasColumnName("parking_area_id");

                entity.Property(e => e.OccupiedSpaces).HasColumnName("occupied_spaces");

                entity.Property(e => e.Timestamp).HasColumnName("timestamp");

                entity.HasOne(d => d.ParkingArea)
                    .WithMany(p => p.ParkingAreaTransactions)
                    .HasForeignKey(d => d.ParkingAreaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("parking_area_transaction_partitioned_parking_area_id_fkey");
            });

            modelBuilder.Entity<ParkingBooth>(entity =>
            {
                entity.ToTable("parking_booth");

                entity.HasIndex(e => new { e.BoothNumber, e.PoiId }, "booth_number_poi_id")
                    .IsUnique();

                entity.Property(e => e.ParkingBoothId)
                    .HasColumnName("parking_booth_id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.BoothNumber).HasColumnName("booth_number");

                entity.Property(e => e.PoiId)
                    .HasColumnName("poi_id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.HasOne(d => d.Poi)
                    .WithMany(p => p.ParkingBooths)
                    .HasForeignKey(d => d.PoiId)
                    .HasConstraintName("parking_booth_poi_id");
            });

            modelBuilder.Entity<ParkingBundle>(entity =>
            {
                entity.HasKey(e => e.PoiId)
                    .HasName("parking_bundle_pkey");

                entity.ToTable("parking_bundle");

                entity.HasIndex(e => e.PappPoiId, "papp_poi_id")
                    .IsUnique();

                entity.Property(e => e.PoiId)
                    .HasColumnName("poi_id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address");

                entity.Property(e => e.Location).HasColumnName("location");

                entity.Property(e => e.PappPoiId).HasColumnName("papp_poi_id");

                entity.Property(e => e.Zip)
                    .HasColumnName("zip")
                    .HasDefaultValueSql("1");

                entity.HasOne(d => d.ZipNavigation)
                    .WithMany(p => p.ParkingBundles)
                    .HasForeignKey(d => d.Zip)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("parking_bundle_zip_code_id_fk");
            });

            modelBuilder.Entity<LegacySensor>(entity =>
            {
                entity.ToTable("sensors");

                entity.Property(e => e.SensorId).HasColumnName("sensor_id");

                entity.Property(e => e.Battery).HasColumnName("battery");

                entity.Property(e => e.InstallationDateTimeEpoch).HasColumnName("installation_date_time_epoch");

                entity.Property(e => e.InstallationTimestamp)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("installation_timestamp");

                entity.Property(e => e.InstalledAtParkingBoothId).HasColumnName("installed_at_parking_booth_id");

                entity.Property(e => e.LastUpdatedBySensorAction).HasColumnName("last_updated_by_sensor_action");

                entity.Property(e => e.LastUpdatedTimestamp)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("last_updated_timestamp");

                entity.Property(e => e.Occupied).HasColumnName("occupied");

                entity.Property(e => e.SensorTypeId).HasColumnName("sensor_type_id");

                entity.HasOne(d => d.InstalledAtParkingBooth)
                    .WithMany(p => p.LegacySensors)
                    .HasForeignKey(d => d.InstalledAtParkingBoothId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("sensors_installed_at_parking_booth_id");

                entity.HasOne(d => d.LastUpdatedBySensorActionNavigation)
                    .WithMany(p => p.LegacySensors)
                    .HasForeignKey(d => d.LastUpdatedBySensorAction)
                    .HasConstraintName("sensors_last_updated_by_sensor_action_fkey");

                entity.HasOne(d => d.SensorType)
                    .WithMany(p => p.LegacySensors)
                    .HasForeignKey(d => d.SensorTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Sensor Type ID");
            });

            modelBuilder.Entity<Sensor>(entity =>
            {
                entity.ToTable("sensor");

                entity.HasComment("Table that contains all sensors.");

                entity.HasIndex(e => e.Id, "sensor_id_uindex")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("character varying")
                    .HasColumnName("id");

                entity.Property(e => e.LatestBattery)
                    .HasColumnName("latest_battery")
                    .HasComment("The latest battery update. If null, none has occurred or DB error.");

                entity.Property(e => e.LatestBatteryTimestamp)
                    .HasColumnName("latest_battery_timestamp")
                    .HasComment("The time of the latest battery update. If null, none has occurred or DB error.");

                entity.Property(e => e.LatestOccupied)
                    .HasColumnName("latest_occupied")
                    .HasComment("Latest occupied update. If null, then none has been recorded or DB error.");

                entity.Property(e => e.LatestOccupiedTimestamp)
                    .HasColumnName("latest_occupied_timestamp")
                    .HasComment("The time of the latest occupied status update. If null, none has occurred or DB error.");

                entity.Property(e => e.Type).HasColumnName("type");

                entity.HasOne(d => d.TypeNavigation)
                    .WithMany(p => p.Sensors)
                    .HasForeignKey(d => d.Type)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("type");
            });

            modelBuilder.Entity<SensorActionOccupied>(entity =>
            {
                entity.HasKey(e => e.OsaId)
                    .HasName("sensor_action_occupied_pkey");

                entity.ToTable("sensor_action_occupied");

                entity.Property(e => e.OsaId)
                    .HasColumnName("osa_id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.ActionTimestamp)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("action_timestamp");

                entity.Property(e => e.Occupied).HasColumnName("occupied");

                entity.Property(e => e.SensorId)
                    .IsRequired()
                    .HasColumnName("sensor_id");

                entity.HasOne(d => d.LegacySensor)
                    .WithMany(p => p.SensorActionOccupieds)
                    .HasForeignKey(d => d.SensorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("sensor_id");
            });

            modelBuilder.Entity<SensorActionsRaw>(entity =>
            {
                entity.HasKey(e => e.ActionId)
                    .HasName("sensor_actions_raw_pkey");

                entity.ToTable("sensor_actions_raw");

                entity.Property(e => e.ActionId)
                    .HasColumnName("action_id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.SensorAction)
                    .IsRequired()
                    .HasColumnType("json")
                    .HasColumnName("sensor_action");
            });

            modelBuilder.Entity<SensorBatteryUpdate>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.SensorId })
                    .HasName("sensor_battery_update_pkey");

                entity.ToTable("sensor_battery_update");

                entity.HasIndex(e => e.SensorId, "sensor_battery_update_sensor_id_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.SensorId)
                    .HasColumnType("character varying")
                    .HasColumnName("sensor_id");

                entity.Property(e => e.Battery).HasColumnName("battery");

                entity.Property(e => e.Ts).HasColumnName("ts");

                entity.HasOne(d => d.Sensor)
                    .WithMany(p => p.SensorBatteryUpdates)
                    .HasForeignKey(d => d.SensorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("sensor_battery_update_new_sensor_id_fkey");
            });

            modelBuilder.Entity<SensorInstall>(entity =>
            {
                entity.ToTable("sensor_install");

                entity.HasComment("Table with all sensor installations. Here one physical sensor could be installed more than once, but not at the same time.");

                entity.HasIndex(e => e.Id, "sensor_install_id_uindex")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Booth).HasColumnName("booth");

                entity.Property(e => e.InstallImage).HasColumnName("install_image");

                entity.Property(e => e.InstallTs)
                    .HasColumnName("install_ts")
                    .HasComment("Time of installation.");

                entity.Property(e => e.SensorId)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("sensor_id")
                    .HasComment("The physical sensor installed in this entry.");

                entity.Property(e => e.UninstallTs)
                    .HasColumnName("uninstall_ts")
                    .HasComment("If null, then the sensor is still active.");

                entity.HasOne(d => d.BoothNavigation)
                    .WithMany(p => p.SensorInstalls)
                    .HasForeignKey(d => d.Booth)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("sensor_install_booth_id_fk");

                entity.HasOne(d => d.InstallImageNavigation)
                    .WithMany(p => p.SensorInstalls)
                    .HasForeignKey(d => d.InstallImage)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("sensor_install_image_id_fk");

                entity.HasOne(d => d.Sensor)
                    .WithMany(p => p.SensorInstalls)
                    .HasForeignKey(d => d.SensorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("sensor_install_sensor_id_fk");
            });

            modelBuilder.Entity<SensorType>(entity =>
            {
                entity.ToTable("sensor_types");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.Manufacturer).HasColumnName("manufacturer");

                entity.Property(e => e.Model)
                    .IsRequired()
                    .HasColumnName("model");

                entity.HasOne(d => d.ManufacturerNavigation)
                    .WithMany(p => p.SensorTypes)
                    .HasForeignKey(d => d.Manufacturer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("sensor_types_manufacturer_id_fk");
            });

            modelBuilder.Entity<SensorUpdate>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.SensorId })
                    .HasName("sensor_update_pkey");

                entity.ToTable("sensor_update");

                entity.HasIndex(e => e.SensorId, "sensor_update_sensor_id_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.SensorId)
                    .HasColumnType("character varying")
                    .HasColumnName("sensor_id");

                entity.Property(e => e.Occupied).HasColumnName("occupied");

                entity.Property(e => e.Ts).HasColumnName("ts");

                entity.HasOne(d => d.Sensor)
                    .WithMany(p => p.SensorUpdates)
                    .HasForeignKey(d => d.SensorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("sensor_update_new_sensor_id_fkey");
            });

            modelBuilder.Entity<ZipCode>(entity =>
            {
                entity.ToTable("zip_code");

                entity.HasComment("Table that contains all zip codes and names");

                entity.HasIndex(e => e.Code, "zip_code_code_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.Id, "zip_code_id_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.Name, "zip_code_name_uindex")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnName("code");

                entity.Property(e => e.CountryId).HasColumnName("country_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.ZipCodes)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("zip_code_country_iso-3166-numeric_fk");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
