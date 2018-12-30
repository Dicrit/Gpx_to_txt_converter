#ifndef GPXCONVERTER_H
#define GPXCONVERTER_H
#include <qstring.h>
#include <QObject>
#include <QtCore>

class GPXConverter : public QObject{
 
	Q_OBJECT
public:
	void ConvertGpx(const QString& inputPath, const QString& outputPath);
signals:
	void Progress(int val);
};
#endif // GPXCONVERTER_H
