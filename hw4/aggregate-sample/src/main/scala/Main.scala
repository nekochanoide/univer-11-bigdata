import com.github.tototoshi.csv._
import java.io._
import scala.language.postfixOps

// Ну типо ок...
implicit object MyFormat extends DefaultCSVFormat {
  override val delimiter = '\t'
}

@main def hello(path: String, filmId: String, out: String): Unit = 
  val reader = CSVReader.open(new File(path))
  val rows = reader.all()
  def allEntries = rows.groupBy(_(2)).mapValues(_.size)
  def filmEntries = rows.filter(_(1) == filmId).groupBy(_(2)).mapValues(_.size)
  val json = s"{\"hist_film\":[${filmEntries("1")},${filmEntries("2")},${filmEntries("3")},${filmEntries("4")},${filmEntries("5")}],\"hist_all\":[${allEntries("1")},${allEntries("2")},${allEntries("3")},${allEntries("4")},${allEntries("5")}]}";
  reader.close()
  
  val pw = new PrintWriter(new File(out))
  pw.write(json)
  pw.close
