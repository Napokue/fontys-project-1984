CREATE DATABASE replacement_words_service WITH OWNER "Admin";

\connect replacement_words_service

CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE TABLE IF NOT EXISTS replacement_words
(
    id       uuid DEFAULT uuid_generate_v4() NOT NULL,
    oldspeak text                            NOT NULL,
    newspeak text                            NOT NULL,
    PRIMARY KEY (id, oldspeak)
);

ALTER TABLE replacement_words
    OWNER TO "Admin";

INSERT INTO public.replacement_words (id, oldspeak, newspeak)
VALUES (DEFAULT, 'war'::text, 'peace'::text);

INSERT INTO public.replacement_words (id, oldspeak, newspeak)
VALUES (DEFAULT, 'freedom'::text, 'slavery'::text);

INSERT INTO public.replacement_words (id, oldspeak, newspeak)
VALUES (DEFAULT, 'ignorance'::text, 'strength'::text);
