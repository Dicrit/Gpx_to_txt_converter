#include "gpxconverter.h"
#include <QFile>
#include <QMessageBox>
#include <qdom.h>
#include <qtextstream.h>


void GPXConverter::ConvertGpx(const QString &inputPath, const QString &outputPath)
{
	QFile inputFile(inputPath);
	QFile outputFile(outputPath);
	
    if (!inputFile.open(QIODevice::ReadOnly)) {
        QMessageBox m;
        m.setText("error read file");
        m.exec();
        return;
    }
    if (!outputFile.open(QIODevice::WriteOnly))
    {
        QMessageBox m;
        m.setText("error create file");
        m.exec();
        return;
    }

	QTextStream output(&outputFile);
    QDomDocument doc;
    doc.setContent(&inputFile);
    QDomElement root = doc.documentElement();
    QDomNodeList children = root.childNodes();
    int size = children.length();
	Progress(0);
    for (int i = 0; i < size; i++)
    {
        QDomElement wpt = children.at(i).toElement();
        QString lat = wpt.attribute("lat");
        QString lon = wpt.attribute("lon");
        QString name = wpt.firstChildElement("name").text();
        output << name << "," << lon << "," << lat << "\r\n";
		Progress((i+1) / (float)size * 100.);
    }
	outputFile.close();
	inputFile.close();
}
