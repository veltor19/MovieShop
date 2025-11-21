IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251117183328_InitialMigration'
)
BEGIN
    CREATE TABLE [Genres] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Genres] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251117183328_InitialMigration'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251117183328_InitialMigration', N'9.0.11');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251117191540_ChangedGenreTable'
)
BEGIN
    ALTER TABLE [Genres] DROP CONSTRAINT [PK_Genres];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251117191540_ChangedGenreTable'
)
BEGIN
    EXEC sp_rename N'[Genres]', N'Genre', 'OBJECT';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251117191540_ChangedGenreTable'
)
BEGIN
    DECLARE @var sysname;
    SELECT @var = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Genre]') AND [c].[name] = N'Name');
    IF @var IS NOT NULL EXEC(N'ALTER TABLE [Genre] DROP CONSTRAINT [' + @var + '];');
    ALTER TABLE [Genre] ALTER COLUMN [Name] nvarchar(64) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251117191540_ChangedGenreTable'
)
BEGIN
    ALTER TABLE [Genre] ADD CONSTRAINT [PK_Genre] PRIMARY KEY ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251117191540_ChangedGenreTable'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251117191540_ChangedGenreTable', N'9.0.11');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251117201452_CreatedMoviesTable'
)
BEGIN
    CREATE TABLE [Movies] (
        [Id] int NOT NULL IDENTITY,
        [BackdropUrl] nvarchar(max) NULL,
        [Budget] decimal(18,2) NOT NULL,
        [CreatedBy] nvarchar(max) NULL,
        [CreatedDate] datetime2 NOT NULL,
        [ImdbURL] nvarchar(max) NULL,
        [OriginalLanguage] nvarchar(max) NULL,
        [Overview] varchar(20) NULL,
        [PosterUrl] nvarchar(max) NULL,
        [Price] decimal(18,2) NOT NULL,
        [ReleaseDate] datetime2 NOT NULL,
        [Revenue] decimal(18,2) NOT NULL,
        [RunTime] int NULL,
        [TagLine] nvarchar(max) NULL,
        [Title] varchar(500) NULL,
        [TmdbUrl] nvarchar(max) NULL,
        [UpdatedBy] nvarchar(max) NULL,
        [UpdateDate] datetime2 NULL,
        CONSTRAINT [PK_Movies] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251117201452_CreatedMoviesTable'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251117201452_CreatedMoviesTable', N'9.0.11');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251117202129_CreatedTrailersTable'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251117202129_CreatedTrailersTable', N'9.0.11');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251117202241_CreateddTrailersTable'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251117202241_CreateddTrailersTable', N'9.0.11');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251117202402_CreatedddTrailersTable'
)
BEGIN
    CREATE TABLE [Trailers] (
        [Id] int NOT NULL IDENTITY,
        [TrailerUrl] nvarchar(256) NOT NULL,
        [Name] nvarchar(256) NOT NULL,
        [MovieId] int NOT NULL,
        CONSTRAINT [PK_Trailers] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Trailers_Movies_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movies] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251117202402_CreatedddTrailersTable'
)
BEGIN
    CREATE INDEX [IX_Trailers_MovieId] ON [Trailers] ([MovieId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251117202402_CreatedddTrailersTable'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251117202402_CreatedddTrailersTable', N'9.0.11');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251117204047_CreatedMovieGenreJunctionTable'
)
BEGIN
    CREATE TABLE [MovieGenres] (
        [GenreId] int NOT NULL,
        [MovieId] int NOT NULL,
        CONSTRAINT [PK_MovieGenres] PRIMARY KEY ([MovieId], [GenreId]),
        CONSTRAINT [FK_MovieGenres_Genre_GenreId] FOREIGN KEY ([GenreId]) REFERENCES [Genre] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_MovieGenres_Movies_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movies] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251117204047_CreatedMovieGenreJunctionTable'
)
BEGIN
    CREATE INDEX [IX_MovieGenres_GenreId] ON [MovieGenres] ([GenreId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251117204047_CreatedMovieGenreJunctionTable'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251117204047_CreatedMovieGenreJunctionTable', N'9.0.11');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251118011629_CreatedCastTable'
)
BEGIN
    CREATE TABLE [Casts] (
        [Id] int NOT NULL IDENTITY,
        [Gender] nvarchar(max) NOT NULL,
        [Name] nvarchar(128) NOT NULL,
        [ProfilePath] nvarchar(2084) NOT NULL,
        [TmdbUrl] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Casts] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251118011629_CreatedCastTable'
)
BEGIN
    CREATE TABLE [MovieCast] (
        [Id] int NOT NULL IDENTITY,
        [Character] nvarchar(max) NOT NULL,
        [MovieId] int NOT NULL,
        [CastId] int NOT NULL,
        CONSTRAINT [PK_MovieCast] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_MovieCast_Casts_CastId] FOREIGN KEY ([CastId]) REFERENCES [Casts] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251118011629_CreatedCastTable'
)
BEGIN
    CREATE INDEX [IX_MovieCast_CastId] ON [MovieCast] ([CastId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251118011629_CreatedCastTable'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251118011629_CreatedCastTable', N'9.0.11');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251118012808_UpdatedMovieCastsTable'
)
BEGIN
    ALTER TABLE [MovieCast] DROP CONSTRAINT [FK_MovieCast_Casts_CastId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251118012808_UpdatedMovieCastsTable'
)
BEGIN
    ALTER TABLE [MovieCast] DROP CONSTRAINT [PK_MovieCast];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251118012808_UpdatedMovieCastsTable'
)
BEGIN
    DROP INDEX [IX_MovieCast_CastId] ON [MovieCast];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251118012808_UpdatedMovieCastsTable'
)
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[MovieCast]') AND [c].[name] = N'Id');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [MovieCast] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [MovieCast] DROP COLUMN [Id];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251118012808_UpdatedMovieCastsTable'
)
BEGIN
    EXEC sp_rename N'[MovieCast]', N'MovieCasts', 'OBJECT';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251118012808_UpdatedMovieCastsTable'
)
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[MovieCasts]') AND [c].[name] = N'Character');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [MovieCasts] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [MovieCasts] ALTER COLUMN [Character] nvarchar(450) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251118012808_UpdatedMovieCastsTable'
)
BEGIN
    ALTER TABLE [MovieCasts] ADD CONSTRAINT [PK_MovieCasts] PRIMARY KEY ([CastId], [MovieId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251118012808_UpdatedMovieCastsTable'
)
BEGIN
    CREATE INDEX [IX_MovieCasts_MovieId] ON [MovieCasts] ([MovieId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251118012808_UpdatedMovieCastsTable'
)
BEGIN
    ALTER TABLE [MovieCasts] ADD CONSTRAINT [FK_MovieCasts_Casts_CastId] FOREIGN KEY ([CastId]) REFERENCES [Casts] ([Id]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251118012808_UpdatedMovieCastsTable'
)
BEGIN
    ALTER TABLE [MovieCasts] ADD CONSTRAINT [FK_MovieCasts_Movies_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movies] ([Id]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251118012808_UpdatedMovieCastsTable'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251118012808_UpdatedMovieCastsTable', N'9.0.11');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251118015948_CreatedUsersTable'
)
BEGIN
    CREATE TABLE [Users] (
        [Id] int NOT NULL IDENTITY,
        [DateOfBirth] datetime2 NOT NULL,
        [Email] nvarchar(256) NOT NULL,
        [FirstName] nvarchar(128) NOT NULL,
        [HashedPassword] nvarchar(1024) NOT NULL,
        [IsLocked] bit NOT NULL,
        [LastName] nvarchar(128) NOT NULL,
        [PhoneNumber] nvarchar(16) NOT NULL,
        [ProfilePictureUrl] nvarchar(max) NOT NULL,
        [Salt] nvarchar(1024) NOT NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251118015948_CreatedUsersTable'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251118015948_CreatedUsersTable', N'9.0.11');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251118020441_CreatedRolesTable'
)
BEGIN
    CREATE TABLE [Roles] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(20) NOT NULL,
        CONSTRAINT [PK_Roles] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251118020441_CreatedRolesTable'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251118020441_CreatedRolesTable', N'9.0.11');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251118021308_CreatedFavoritesTable'
)
BEGIN
    CREATE TABLE [Favorites] (
        [MovieId] int NOT NULL,
        [UserId] int NOT NULL,
        CONSTRAINT [PK_Favorites] PRIMARY KEY ([UserId], [MovieId]),
        CONSTRAINT [FK_Favorites_Movies_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movies] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Favorites_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251118021308_CreatedFavoritesTable'
)
BEGIN
    CREATE INDEX [IX_Favorites_MovieId] ON [Favorites] ([MovieId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251118021308_CreatedFavoritesTable'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251118021308_CreatedFavoritesTable', N'9.0.11');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251118022203_CreatedReviewsTable'
)
BEGIN
    CREATE TABLE [Reviews] (
        [MovieId] int NOT NULL,
        [UserId] int NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [Rating] decimal(18,2) NOT NULL,
        [ReviewText] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Reviews] PRIMARY KEY ([MovieId], [UserId]),
        CONSTRAINT [FK_Reviews_Movies_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movies] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Reviews_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251118022203_CreatedReviewsTable'
)
BEGIN
    CREATE INDEX [IX_Reviews_UserId] ON [Reviews] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251118022203_CreatedReviewsTable'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251118022203_CreatedReviewsTable', N'9.0.11');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251118022738_CreatedPurchasesTable'
)
BEGIN
    CREATE TABLE [Purchases] (
        [MovieId] int NOT NULL,
        [UserId] int NOT NULL,
        [PurchaseDateTime] datetime2 NOT NULL,
        [PurchaseNumber] nvarchar(max) NOT NULL,
        [TotalPrice] decimal(18,2) NOT NULL,
        CONSTRAINT [PK_Purchases] PRIMARY KEY ([UserId], [MovieId]),
        CONSTRAINT [FK_Purchases_Movies_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movies] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Purchases_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251118022738_CreatedPurchasesTable'
)
BEGIN
    CREATE INDEX [IX_Purchases_MovieId] ON [Purchases] ([MovieId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251118022738_CreatedPurchasesTable'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251118022738_CreatedPurchasesTable', N'9.0.11');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251118023138_CreatedUserRolesTable'
)
BEGIN
    CREATE TABLE [UserRoles] (
        [RoleId] int NOT NULL,
        [UserId] int NOT NULL,
        CONSTRAINT [PK_UserRoles] PRIMARY KEY ([RoleId], [UserId]),
        CONSTRAINT [FK_UserRoles_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_UserRoles_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251118023138_CreatedUserRolesTable'
)
BEGIN
    CREATE INDEX [IX_UserRoles_UserId] ON [UserRoles] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251118023138_CreatedUserRolesTable'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251118023138_CreatedUserRolesTable', N'9.0.11');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251118024857_ChangedGenreToGenres'
)
BEGIN
    ALTER TABLE [MovieGenres] DROP CONSTRAINT [FK_MovieGenres_Genre_GenreId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251118024857_ChangedGenreToGenres'
)
BEGIN
    ALTER TABLE [Genre] DROP CONSTRAINT [PK_Genre];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251118024857_ChangedGenreToGenres'
)
BEGIN
    EXEC sp_rename N'[Genre]', N'Genres', 'OBJECT';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251118024857_ChangedGenreToGenres'
)
BEGIN
    ALTER TABLE [Genres] ADD CONSTRAINT [PK_Genres] PRIMARY KEY ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251118024857_ChangedGenreToGenres'
)
BEGIN
    ALTER TABLE [MovieGenres] ADD CONSTRAINT [FK_MovieGenres_Genres_GenreId] FOREIGN KEY ([GenreId]) REFERENCES [Genres] ([Id]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251118024857_ChangedGenreToGenres'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251118024857_ChangedGenreToGenres', N'9.0.11');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251118025712_UpdatedMoviesColumnTypes'
)
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movies]') AND [c].[name] = N'TmdbUrl');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Movies] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [Movies] ALTER COLUMN [TmdbUrl] nvarchar(2084) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251118025712_UpdatedMoviesColumnTypes'
)
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movies]') AND [c].[name] = N'Title');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Movies] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [Movies] ALTER COLUMN [Title] nvarchar(256) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251118025712_UpdatedMoviesColumnTypes'
)
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movies]') AND [c].[name] = N'TagLine');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Movies] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [Movies] ALTER COLUMN [TagLine] nvarchar(512) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251118025712_UpdatedMoviesColumnTypes'
)
BEGIN
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movies]') AND [c].[name] = N'Revenue');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Movies] DROP CONSTRAINT [' + @var6 + '];');
    ALTER TABLE [Movies] ALTER COLUMN [Revenue] decimal(18,4) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251118025712_UpdatedMoviesColumnTypes'
)
BEGIN
    DECLARE @var7 sysname;
    SELECT @var7 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movies]') AND [c].[name] = N'Price');
    IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Movies] DROP CONSTRAINT [' + @var7 + '];');
    ALTER TABLE [Movies] ALTER COLUMN [Price] decimal(5,2) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251118025712_UpdatedMoviesColumnTypes'
)
BEGIN
    DECLARE @var8 sysname;
    SELECT @var8 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movies]') AND [c].[name] = N'PosterUrl');
    IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Movies] DROP CONSTRAINT [' + @var8 + '];');
    ALTER TABLE [Movies] ALTER COLUMN [PosterUrl] nvarchar(2084) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251118025712_UpdatedMoviesColumnTypes'
)
BEGIN
    DECLARE @var9 sysname;
    SELECT @var9 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movies]') AND [c].[name] = N'Overview');
    IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [Movies] DROP CONSTRAINT [' + @var9 + '];');
    ALTER TABLE [Movies] ALTER COLUMN [Overview] nvarchar(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251118025712_UpdatedMoviesColumnTypes'
)
BEGIN
    DECLARE @var10 sysname;
    SELECT @var10 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movies]') AND [c].[name] = N'OriginalLanguage');
    IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [Movies] DROP CONSTRAINT [' + @var10 + '];');
    ALTER TABLE [Movies] ALTER COLUMN [OriginalLanguage] nvarchar(64) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251118025712_UpdatedMoviesColumnTypes'
)
BEGIN
    DECLARE @var11 sysname;
    SELECT @var11 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movies]') AND [c].[name] = N'ImdbURL');
    IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [Movies] DROP CONSTRAINT [' + @var11 + '];');
    ALTER TABLE [Movies] ALTER COLUMN [ImdbURL] nvarchar(2084) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251118025712_UpdatedMoviesColumnTypes'
)
BEGIN
    DECLARE @var12 sysname;
    SELECT @var12 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movies]') AND [c].[name] = N'Budget');
    IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [Movies] DROP CONSTRAINT [' + @var12 + '];');
    ALTER TABLE [Movies] ALTER COLUMN [Budget] decimal(18,4) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251118025712_UpdatedMoviesColumnTypes'
)
BEGIN
    DECLARE @var13 sysname;
    SELECT @var13 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movies]') AND [c].[name] = N'BackdropUrl');
    IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [Movies] DROP CONSTRAINT [' + @var13 + '];');
    ALTER TABLE [Movies] ALTER COLUMN [BackdropUrl] nvarchar(2084) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251118025712_UpdatedMoviesColumnTypes'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251118025712_UpdatedMoviesColumnTypes', N'9.0.11');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251118030028_MadeTypesNullableInMovies'
)
BEGIN
    DECLARE @var14 sysname;
    SELECT @var14 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movies]') AND [c].[name] = N'CreatedDate');
    IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [Movies] DROP CONSTRAINT [' + @var14 + '];');
    ALTER TABLE [Movies] ALTER COLUMN [CreatedDate] datetime2 NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251118030028_MadeTypesNullableInMovies'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251118030028_MadeTypesNullableInMovies', N'9.0.11');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251118030146_MadeTypesNullableInMoviesForReal'
)
BEGIN
    DECLARE @var15 sysname;
    SELECT @var15 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movies]') AND [c].[name] = N'Revenue');
    IF @var15 IS NOT NULL EXEC(N'ALTER TABLE [Movies] DROP CONSTRAINT [' + @var15 + '];');
    ALTER TABLE [Movies] ALTER COLUMN [Revenue] decimal(18,4) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251118030146_MadeTypesNullableInMoviesForReal'
)
BEGIN
    DECLARE @var16 sysname;
    SELECT @var16 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movies]') AND [c].[name] = N'ReleaseDate');
    IF @var16 IS NOT NULL EXEC(N'ALTER TABLE [Movies] DROP CONSTRAINT [' + @var16 + '];');
    ALTER TABLE [Movies] ALTER COLUMN [ReleaseDate] datetime2 NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251118030146_MadeTypesNullableInMoviesForReal'
)
BEGIN
    DECLARE @var17 sysname;
    SELECT @var17 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movies]') AND [c].[name] = N'Price');
    IF @var17 IS NOT NULL EXEC(N'ALTER TABLE [Movies] DROP CONSTRAINT [' + @var17 + '];');
    ALTER TABLE [Movies] ALTER COLUMN [Price] decimal(5,2) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251118030146_MadeTypesNullableInMoviesForReal'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251118030146_MadeTypesNullableInMoviesForReal', N'9.0.11');
END;

COMMIT;
GO

