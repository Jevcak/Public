/*
namespace ParagraphCountingTests
{
    public class ParagraphCountingTest
    {
        [Fact]
        public void Nothing()
        {
            //Arrange
            string input = "";
            var reader = new StringReader(input);
            var writer = new StringWriter();
            var counter = new ParagraphCounter(reader, writer);


            //Act
            counter.Count();


            //Assert

            Assert.Equal("", writer.ToString());
        }
        public void JustWhiteSpaces()
        {
            //Arrange
            string input = """

                   

                   

                   
                """;
            var reader = new StringReader(input);
            var writer = new StringWriter();
            var counter = new ParagraphCounter(reader, writer);


            //Act
            counter.Count();


            //Assert

            Assert.Equal("", writer.ToString());
        }
        [Fact]
        public void OneParagraph()
        {
            //Arrange
            string input = """
            Started earnest brother believe an exposed so. 
            Me he believing daughters if forfeited at furniture. 
            Age again and stuff downs spoke. Late hour new nay able 
            fat each sell. Nor themselves age introduced frequently 
            use unsatiable devonshire get. They why quit gay cold rose deal park.
            """;
            var reader = new StringReader(input);
            var writer = new StringWriter();
            var counter = new ParagraphCounter(reader, writer);


            //Act
            counter.Count();


            //Assert

            Assert.Equal("46" + writer.NewLine, writer.ToString());
        }
        [Fact]
        public void TwoParagraphs()
        {
            //Arrange
            string input = """
            Started earnest brother believe an exposed so. 
            Me he believing daughters if forfeited at furniture. 

            Age again and stuff downs spoke. Late hour new nay able 
            fat each sell. Nor themselves age introduced frequently 
            use unsatiable devonshire get. They why quit gay cold rose deal park.
            """;
            var reader = new StringReader(input);
            var writer = new StringWriter();
            var counter = new ParagraphCounter(reader, writer);


            //Act
            counter.Count();


            //Assert

            Assert.Equal("15" + writer.NewLine + "31" + writer.NewLine, writer.ToString());
        }
        [Fact]
        public void MoreSpacesBetweenParagraphs()
        {
            //Arrange
            string input = """
            Started earnest brother believe an exposed so. 
            Me he believing daughters if forfeited at furniture. 




            Age again and stuff downs spoke. Late hour new nay able 
            fat each sell. Nor themselves age introduced frequently 
            use unsatiable devonshire get. They why quit gay cold rose deal park.
            """;
            var reader = new StringReader(input);
            var writer = new StringWriter();
            var counter = new ParagraphCounter(reader, writer);


            //Act
            counter.Count();


            //Assert

            Assert.Equal("15" + writer.NewLine + "31" + writer.NewLine, writer.ToString());
        }
        [Fact]
        public void SpacesAtTheBegining()
        {
            //Arrange
            string input = """



            GALLIA est omnis divisa in partes tres, 
            quarum unam incolunt Belgae, aliam Aquitani,
            tertiam qui ipsorum lingua Celtae, nostra Galli
            appellantur. Hi omnes lingua, institutis, legibus
            inter se differunt.
            """; ;
            var reader = new StringReader(input);
            var writer = new StringWriter();    
            var counter = new ParagraphCounter(reader, writer);


            //Act
            counter.Count();


            //Assert

            Assert.Equal("29" + writer.NewLine, writer.ToString());
        }
        [Fact]
        public void NormalText()
        {
            //Arrange
            string input = """
            GALLIA est omnis divisa in partes tres, 
            quarum unam incolunt Belgae, aliam Aquitani,
            tertiam qui ipsorum lingua Celtae, nostra Galli
            appellantur. Hi omnes lingua, institutis, legibus
            inter se differunt.

            Gallos ab Aquitanis Garumna flumen,
            a Belgis Matrona et Sequana dividit.

            Horum omnium fortissimi sunt Belgae,
            propterea quod a cultu atque humanitate
            provinciae longissime absunt, minimeque
            ad eos mercatores saepe commeant atque 
            ea quae ad effeminandos animos pertinent
            important, proximique sunt Germanis, qui
            trans Rhenum incolunt, quibuscum continenter
            bellum gerunt. Qua de causa Helvetii quoque
            reliquos Gallos virtute praecedunt, quod 
            fere cotidianis proeliis cum Germanis 
            contendunt, cum aut suis finibus eos 
            prohibent aut ipsi in eorum finibus 
            bellum gerunt.

            [Eorum una, pars, quam Gallos obtinere
            dictum est, initium capit a flumine 
            Rhodano, continetur Garumna flumine,
            Oceano, finibus Belgarum, attingit
            etiam ab Sequanis et Helvetiis flumen
            Rhenum, vergit ad septentriones.
            """; ;
            var reader = new StringReader(input);
            var writer = new StringWriter();
            var counter = new ParagraphCounter(reader, writer);


            //Act
            counter.Count();


            //Assert

            Assert.Equal("29" + writer.NewLine + "11" + writer.NewLine + "68" + writer.NewLine + "30" + writer.NewLine, writer.ToString());
        }
        [Fact]
        public void LongerTextMoreWords()
        {
            //Arrange
            string input = """
            GALLIA est omnis divisa in partes tres, 
            quarum unam incolunt Belgae, aliam Aquitani,
            tertiam qui ipsorum lingua Celtae, nostra Galli
            appellantur. Hi omnes lingua, institutis, legibus
            inter se differunt.

            Gallos ab Aquitanis Garumna flumen,
            a Belgis Matrona et Sequana dividit.

            Horum omnium fortissimi sunt Belgae,
            propterea quod a cultu atque humanitate
            provinciae longissime absunt, minimeque
            ad eos mercatores saepe commeant atque 
            ea quae ad effeminandos animos pertinent
            important, proximique sunt Germanis, qui
            trans Rhenum incolunt, quibuscum continenter
            bellum gerunt. Qua de causa Helvetii quoque
            reliquos Gallos virtute praecedunt, quod 
            fere cotidianis proeliis cum Germanis 
            contendunt, cum aut suis finibus eos 
            prohibent aut ipsi in eorum finibus 
            bellum gerunt.

            [Eorum una, pars, quam Gallos obtinere
            dictum est, initium capit a flumine 
            Rhodano, continetur Garumna flumine,
            Oceano, finibus Belgarum, attingit
            etiam ab Sequanis et Helvetiis flumen
            Rhenum, vergit ad septentriones.

            Belgae ab extremis Galliae finibus oriuntur,
            pertinent ad inferiorem partem fluminis Rheni,
            spectant in septentrionem et orientem solem.

            Aquitania a Garumna flumine ad Pyrenaeos 
            montes et eam partem Oceani quae est ad 
            Hispaniam pertinet; spectat inter occasum
            solis et septentriones.]

            Apud Helvetios longe nobilissimus fuit et
            ditissimus Orgetorix. Is M. Messala, M. Pisone
            consulibus regni cupiditate inductus 
            coniurationem nobilitatis fecit et civitati
            persuasit ut de finibus suis cum omnibus 
            copiis exirent: perfacile esse, cum virtute
            omnibus praestarent, totius Galliae imperio
            potiri. Id hoc facilius iis persuasit, quod
            undique loci natura Helvetii continentur: 
            una ex parte flumine Rheno latissimo atque 
            altissimo, qui agrum Helvetium a Germanis 
            dividit; altera ex parte monte Iura altissimo,
            qui est inter Sequanos et Helvetios; tertia 
            lacu Lemanno et flumine Rhodano, qui provinciam
            nostram ab Helvetiis dividit. His rebus fiebat
            ut et minus late vagarentur et minus facile 
            finitimis bellum inferre possent; qua ex parte
            homines bellandi cupidi magno dolore adficiebantur.
            Pro multitudine autem hominum et pro gloria belli
            atque fortitudinis angustos se fines habere
            arbitrabantur, qui in longitudinem milia passuum
            CCXL, in latitudinem CLXXX patebant.

            His rebus adducti et auctoritate Orgetorigis
            permoti constituerunt ea quae ad proficiscendum
            pertinerent comparare, iumentorum et carrorum
            quam maximum numerum coemere, sementes quam
            maximas facere, ut in itinere copia frumenti
            suppeteret, cum proximis civitatibus pacem
            et amicitiam confirmare. Ad eas res conficiendas
            biennium sibi satis esse duxerunt; in tertium
            annum profectionem lege confirmant. Ad eas res
            conficiendas Orgetorix deligitur. Is sibi 
            legationem ad civitates suscipit. In eo 
            itinere persuadet Castico, Catamantaloedis
            filio, Sequano, cuius pater regnum in 
            Sequanis multos annos obtinuerat et a 
            senatu populi Romani amicus appellatus 
            erat, ut regnum in civitate sua occuparet,
            quod pater ante habuerit; itemque Dumnorigi
            Haeduo, fratri Diviciaci, qui eo tempore
            principatum in civitate obtinebat ac maxime
            plebi acceptus erat, ut idem conaretur 
            persuadet eique filiam suam in matrimonium
            dat. Perfacile factu esse illis probat 
            conata perficere, propterea quod ipse suae
            civitatis imperium obtenturus esset: non 
            esse dubium quin totius Galliae plurimum
            Helvetii possent; se suis copiis suoque
            exercitu illis regna conciliaturum confirmat.
            Hac oratione adducti inter se fidem et ius 
            iurandum dant et regno occupato per tres 
            potentissimos ac firmissimos populos totius
            Galliae sese potiri posse sperant.
            """; ;
            var reader = new StringReader(input);
            var writer = new StringWriter();
            var counter = new ParagraphCounter(reader, writer);


            //Act
            counter.Count();


            //Assert

            Assert.Equal("29\r\n11\r\n68\r\n30\r\n18\r\n22\r\n139\r\n184\r\n", writer.ToString());
        }
        [Fact]
        public void LongerTextMoreParagraphs()
        {
            //Arrange
            string input = """
            GALLIA est omnis divisa in partes tres, 

            quarum unam incolunt Belgae, aliam Aquitani,

            tertiam qui ipsorum lingua Celtae, nostra Galli

            appellantur. Hi omnes lingua, institutis, legibus
            inter se differunt.

            Gallos ab Aquitanis Garumna flumen,
            a Belgis Matrona et Sequana dividit.

            Horum omnium fortissimi sunt Belgae,
            propterea quod a cultu atque humanitate
            provinciae longissime absunt, minimeque

            ad eos mercatores saepe commeant atque 
            ea quae ad effeminandos animos pertinent

            important, proximique sunt Germanis, qui
            trans Rhenum incolunt, quibuscum continenter

            bellum gerunt. Qua de causa Helvetii quoque
            reliquos Gallos virtute praecedunt, quod 
            fere cotidianis proeliis cum Germanis 

            contendunt, cum aut suis finibus eos 
            prohibent aut ipsi in eorum finibus 

            bellum gerunt.

            [Eorum una, pars, quam Gallos obtinere
            dictum est, initium capit a flumine 
            Rhodano, continetur Garumna flumine,

            Oceano, finibus Belgarum, attingit
            etiam ab Sequanis et Helvetiis flumen

            Rhenum, vergit ad septentriones.

            Belgae ab extremis Galliae finibus oriuntur,

            pertinent ad inferiorem partem fluminis Rheni,
            spectant in septentrionem et orientem solem.


            Aquitania a Garumna flumine ad Pyrenaeos 
            montes et eam partem Oceani quae est ad 

            Hispaniam pertinet; spectat inter occasum
            solis et septentriones.]

            Apud Helvetios longe nobilissimus fuit et
            ditissimus Orgetorix. Is M. Messala, M. Pisone

            consulibus regni cupiditate inductus 
            coniurationem nobilitatis fecit et civitati
            persuasit ut de finibus suis cum omnibus 

            copiis exirent: perfacile esse, cum virtute
            omnibus praestarent, totius Galliae imperio

            potiri. Id hoc facilius iis persuasit, quod
            undique loci natura Helvetii continentur: 
            una ex parte flumine Rheno latissimo atque 

            altissimo, qui agrum Helvetium a Germanis 
            dividit; altera ex parte monte Iura altissimo,
            qui est inter Sequanos et Helvetios; tertia 

            lacu Lemanno et flumine Rhodano, qui provinciam
            nostram ab Helvetiis dividit. His rebus fiebat
            ut et minus late vagarentur et minus facile 

            finitimis bellum inferre possent; qua ex parte
            homines bellandi cupidi magno dolore adficiebantur.

            Pro multitudine autem hominum et pro gloria belli
            atque fortitudinis angustos se fines habere

            arbitrabantur, qui in longitudinem milia passuum
            CCXL, in latitudinem CLXXX patebant.

            His rebus adducti et auctoritate Orgetorigis
            permoti constituerunt ea quae ad proficiscendum

            pertinerent comparare, iumentorum et carrorum
            quam maximum numerum coemere, sementes quam
            maximas facere, ut in itinere copia frumenti

            suppeteret, cum proximis civitatibus pacem
            et amicitiam confirmare. Ad eas res conficiendas
            biennium sibi satis esse duxerunt; in tertium

            annum profectionem lege confirmant. Ad eas res
            conficiendas Orgetorix deligitur. Is sibi 
            legationem ad civitates suscipit. In eo 

            itinere persuadet Castico, Catamantaloedis
            filio, Sequano, cuius pater regnum in 

            Sequanis multos annos obtinuerat et a 
            senatu populi Romani amicus appellatus 
            erat, ut regnum in civitate sua occuparet,

            quod pater ante habuerit; itemque Dumnorigi
            Haeduo, fratri Diviciaci, qui eo tempore
            principatum in civitate obtinebat ac maxime

            plebi acceptus erat, ut idem conaretur 
            persuadet eique filiam suam in matrimonium
            dat. Perfacile factu esse illis probat 

            conata perficere, propterea quod ipse suae
            civitatis imperium obtenturus esset: non 

            esse dubium quin totius Galliae plurimum
            Helvetii possent; se suis copiis suoque
            exercitu illis regna conciliaturum confirmat.

            Hac oratione adducti inter se fidem et ius 
            iurandum dant et regno occupato per tres 
            potentissimos ac firmissimos populos totius

            Galliae sese potiri posse sperant.
            """; ;
            var reader = new StringReader(input);
            var writer = new StringWriter();
            var counter = new ParagraphCounter(reader, writer);


            //Act
            counter.Count();


            //Assert

            Assert.Equal("7\r\n6\r\n7\r\n9\r\n11\r\n15\r\n12\r\n10\r\n17\r\n12\r\n2\r\n16\r\n10\r\n4\r\n6\r\n12\r\n14\r\n8\r\n13\r\n16\r\n11\r\n19\r\n20\r\n22\r\n13\r\n14\r\n11\r\n12\r\n18\r\n19\r\n18\r\n10\r\n18\r\n18\r\n18\r\n11\r\n17\r\n20\r\n5\r\n", writer.ToString());
        }
    }
}
*/