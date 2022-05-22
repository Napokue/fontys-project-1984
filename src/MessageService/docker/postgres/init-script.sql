CREATE DATABASE message_service WITH OWNER "Admin";

\connect message_service

CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE TABLE IF NOT EXISTS "message"
(
    id           uuid      NOT NULL DEFAULT uuid_generate_v4()
        CONSTRAINT user_pk
            PRIMARY KEY,
    content         text      NOT NULL,
    date_created timestamp NOT NULL DEFAULT NOW()
);

ALTER TABLE "message"
    OWNER TO "Admin";

