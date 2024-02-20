-- Inserisci dati nella tabella Ruolo
-- INSERT INTO Ruolo (Titolo) VALUES ('Admin');
-- INSERT INTO Ruolo (Titolo) VALUES ('User');


-- NON USARE E' DA FINIRE




-- Inserisci dati nella tabella Utente
INSERT INTO Utente (Nome, Cognome, Email, Password, Username) 
VALUES ('Mario', 'Rossi', 'mario@email.com', 'passlord', 'superMario'),
 ('Luca', 'Verdi', 'luca@email.com', 'hola', 'ErLuca');

-- Inserisci dati nella tabella Prodotto
INSERT INTO Prodotto (Nome, Descrizione, Prezzo, ImmagineUrl) VALUES ('Prodotto1', 'Descrizione1', 10.99, 'url1');

INSERT INTO Prodotto (Nome, Descrizione, Prezzo) 
VALUES ('Prodotto2', 'Descrizione2', 20.49);

-- Inserisci dati nella tabella Carrello
INSERT INTO Carrello (UtenteId) VALUES (1); -- Assumendo che 1 sia l'ID dell'utente Mario

-- Inserisci dati nella tabella ProdottoInCarrello
INSERT INTO ProdottoInCarrello (CarrelloId, ProdottoId, Quantita) VALUES (1, 1, 2); -- Assumendo che 1 sia l'ID del carrello e 1 sia l'ID del prodotto "Prodotto1"
