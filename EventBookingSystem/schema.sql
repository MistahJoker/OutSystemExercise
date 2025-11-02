CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);

BEGIN TRANSACTION;
CREATE TABLE "Users" (
    "UserId" INTEGER NOT NULL CONSTRAINT "PK_Users" PRIMARY KEY AUTOINCREMENT
);

CREATE TABLE "Venues" (
    "VenueId" INTEGER NOT NULL CONSTRAINT "PK_Venues" PRIMARY KEY AUTOINCREMENT,
    "Capacity" INTEGER NOT NULL
);

CREATE TABLE "Events" (
    "EventId" INTEGER NOT NULL CONSTRAINT "PK_Events" PRIMARY KEY AUTOINCREMENT,
    "VenueId" INTEGER NOT NULL,
    CONSTRAINT "FK_Events_Venues_VenueId" FOREIGN KEY ("VenueId") REFERENCES "Venues" ("VenueId") ON DELETE CASCADE
);

CREATE TABLE "Bookings" (
    "BookingId" INTEGER NOT NULL CONSTRAINT "PK_Bookings" PRIMARY KEY AUTOINCREMENT,
    "PaymentId" INTEGER NOT NULL,
    "PaymentStatus" TEXT NOT NULL,
    "UserId" INTEGER NOT NULL,
    "EventId" INTEGER NOT NULL,
    CONSTRAINT "FK_Bookings_Events_EventId" FOREIGN KEY ("EventId") REFERENCES "Events" ("EventId") ON DELETE CASCADE,
    CONSTRAINT "FK_Bookings_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "Users" ("UserId") ON DELETE CASCADE
);

CREATE INDEX "IX_Bookings_EventId" ON "Bookings" ("EventId");

CREATE INDEX "IX_Bookings_UserId" ON "Bookings" ("UserId");

CREATE INDEX "IX_Events_VenueId" ON "Events" ("VenueId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20251030164608_InitialCreate', '9.0.10');

ALTER TABLE "Events" ADD "Date" TEXT NOT NULL DEFAULT '0001-01-01 00:00:00';

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20251102025217_AddDateToEvent', '9.0.10');

COMMIT;

