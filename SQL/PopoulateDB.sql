-- Inserisci dati nella tabella Ruolo (Noi usiamo solo questi due ruoli)
-- INSERT INTO Ruolo (Titolo) VALUES ('Admin');
-- INSERT INTO Ruolo (Titolo) VALUES ('User');

-- Inserisci dati nella tabella Utente
INSERT INTO Utente (Nome, Cognome, Email, Password, Username) 
VALUES ('Mario', 'Rossi', 'mario@email.com', 'passlord', 'superMario'),
 ('Luca', 'Verdi', 'luca@email.com', 'hola', 'ErLuca'),
 ('Luca', 'Bianchi', 'luca.bianchi@example.com', 'unAltraPassword123', 'lucaBianchi92'),
 ('Giulia', 'Verdi', 'giulia.verdi@example.com', 'passwordMoltoSicura456', 'giuliaVerdi85'),
 ('Sofia', 'Gialli', 'sofia.gialli@example.com', 'ancoraUnaPassword789', 'sofiaGialli93'),
 ('Marco', 'Neri', 'marco.neri@example.com', 'passwordSuperSicura987', 'marcoNeri89');

-- Inserisci dati nella tabella Prodotto
--INSERT INTO Prodotto (Nome, Descrizione, Prezzo, ImmagineUrl) VALUES ('Prodotto1', 'Descrizione1', 10.99, 'url1');

INSERT INTO Prodotto (Nome, Descrizione, Prezzo) 
VALUES ('Prodotto2', 'Descrizione2', 20.49),
('Scarpe da mani', 'Migliori scarpe per tenere la testa calda', 30.12),
('Telefono', 'Smartphone di ultima generazione con 8GB di RAM', 599.99),
('Laptop', 'Laptop da 15 pollici, adatto per il gaming e il lavoro', 1200.00),
('Cuffie Bluetooth', 'Cuffie senza fili con cancellazione del rumore', 150.00),
('Macchina del Caffè', 'Macchina del caffè espresso automatica con schiumatore per latte', 250.00),
('Tastiera Meccanica', 'Tastiera meccanica retroilluminata con switch blu', 80.00),
('Smartwatch', 'Orologio intelligente con monitoraggio attività e salute', 199.99);

-- Inserisci dati nella tabella Carrello
-- Assumendo che 1 sia l'ID dell'utente Mario
INSERT INTO Carrello (UtenteId) 
VALUES (1),(2),(3),(4),(5);

-- Inserisci dati nella tabella ProdottoInCarrello
-- Assumendo che 1 sia l'ID del carrello e 1 sia l'ID del prodotto "Prodotto1"
INSERT INTO ProdottoInCarrello (CarrelloId, ProdottoId, Quantita) 
VALUES (1, 1, 2),
(2, 5, 10),
(1, 2, 1),
(1, 3, 4),
(1, 6, 7),
(1, 5, 3),
(3, 8, 5),
(4, 6, 2); 
