use datakeepr;

ALTER TABLE vaults
ADD COLUMN img VARCHAR(255);

-- DELETE FROM keeps;
-- DELETE FROM vaults;
-- CREATE TABLE profiles
-- (
--   id VARCHAR(255),
--   email VARCHAR(255),
--   name VARCHAR(255),
--   picture VARCHAR(255),
--   PRIMARY KEY (id)
-- );

-- CREATE TABLE keeps
-- (
-- id INT NOT NULL AUTO_INCREMENT,
-- creatorId VARCHAR(255),
-- name VARCHAR(255),
-- description VARCHAR(255),
-- img VARCHAR(255),
-- views INT NOT NULL DEFAULT 0,
-- shares INT NOT NULL DEFAULT 0,
-- keeps INT NOT NUL DEFAULT 0,
-- PRIMARY KEY (id),
-- FOREIGN KEY (creatorId)
--   REFERENCES profiles (id)
-- );

-- CREATE TABLE vaults
-- (
-- id INT NOT NULL AUTO_INCREMENT,
-- creatorId VARCHAR(255),
-- name VARCHAR(255),
-- description VARCHAR(255),
-- isPrivate TINYINT DEFAULT 0,
-- PRIMARY KEY (id),
-- FOREIGN KEY (creatorId)
--   REFERENCES profiles (id)
--   ON DELETE CASCADE
-- );

-- CREATE TABLE vaultKeeps
-- (
--   id INT NOT NULL AUTO_INCREMENT,
--   creatorId VARCHAR(255),
--   vaultId INT NOT NULL,
--   keepId INT NOT NULL,
--   PRIMARY KEY (id),
--   FOREIGN KEY (creatorId)
--     REFERENCES profiles (id)
--     ON DELETE CASCADE,
--   FOREIGN KEY (vaultId)
--     REFERENCES vaults (id)
--     ON DELETE CASCADE,
--   FOREIGN KEY (keepId)
--     REFERENCES keeps (id)
--     ON DELETE CASCADE
-- )